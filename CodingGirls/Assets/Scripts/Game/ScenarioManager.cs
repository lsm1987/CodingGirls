using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Game
{
    public class ScenarioManager
    {
        private Dictionary<string, ConstructorInfo> _constructorInfos = new Dictionary<string, ConstructorInfo>();
        private List<Command> _commands = new List<Command>();
        private Dictionary<string, int> _labels = new Dictionary<string, int>();    // <라벨명, 커맨드 인덱스>
        private int _commandIdx = 0;    // 실행 시도할 커맨드 인덱스

        public void Initialize()
        {
            Type baseCommandType = typeof(Command);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            IEnumerable<Type> commandTypes = types.Where(t => t.IsSubclassOf(baseCommandType));

            foreach (Type commandType in commandTypes)
            {
                FieldInfo idField = commandType.GetField(CommandUtil._idFieldName);
                string id = idField.GetRawConstantValue() as string;
                ConstructorInfo constructorInfo = commandType.GetConstructor(Type.EmptyTypes);

                RegisterConstructorInfo(id, constructorInfo);
            }
        }

        private void RegisterConstructorInfo(string commandID, ConstructorInfo constructorInfo)
        {
            _constructorInfos.Add(commandID, constructorInfo);
        }

        private Command CreateCommandFromID(string commandID)
        {
            ConstructorInfo constructorInfo;
            if (!_constructorInfos.TryGetValue(commandID, out constructorInfo))
            {
                return null;
            }
            return constructorInfo.Invoke(null) as Command;
        }

        public void Load(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(Define._scenarioRoot + "/" + fileName);
            if (textAsset == null)
            {
                Debug.LogError("[ScenarioLoader.CannotFindFile]" + fileName);
                return;
            }

            _commands.Clear();
            _labels.Clear();
            _commandIdx = 0;

            string text = textAsset.text;
            text = text.Trim().Replace("\r", "");
            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; ++i)
            {
                //Debug.Log("[" + i.ToString() + "] " + lines[i]);
                CommandError error = null;
                Command command = ParseToCommand(lines[i], out error);

                if (error != null)
                {
                    Debug.LogError("[ScenarioLoader.InvalidLine]" + fileName + "(" + i.ToString() + ") " + error
                        + "\n" + lines[i]);
                    continue;
                }
                if (command != null)
                {
                    _commands.Add(command);
                }
            }
            return;
        }

        private Command ParseToCommand(string line, out CommandError error)
        {
            if (line == "" || line.StartsWith("//"))
            {
                error = null;
                return null;
            }

            string commandID;
            string commandParam;
            StringDefine.SplitFirstWord(line, out commandID, out commandParam);

            Command command = CreateCommandFromID(commandID);
            if (command == null)
            {
                error = new CommandInvalidIDError(commandID);
                return null;
            }

            CommandError parseError = null;
            command.Parse(commandParam, out parseError);
            if (parseError != null)
            {
                error = parseError;
                return null;
            }

            CommandError onParsedError = null;
            command.OnParsed(out onParsedError);
            if (onParsedError != null)
            {
                error = parseError;
                return null;
            }

            error = null;
            return command;
        }

        /// <summary>
        /// 라벨을 추가한다.
        /// </summary>
        /// <returns>성공여부</returns>
        private bool AddLabel(string labelName, int commandIdx)
        {
            if (!_labels.ContainsKey(labelName))
            {
                _labels.Add(labelName, commandIdx);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 파싱 후 명령어 목록에 추가 전에 라벨 추가 시도
        /// </summary>
        /// <returns>성공여부</returns>
        public bool AddLabelOnParsed(string labelName)
        {
            int commandIdx = _commands.Count;
            if (!AddLabel(labelName, commandIdx))
            {
                Debug.LogError("[ScenarioLoader.AlreadyAddedLabel]" + labelName);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 지정한 이름의 라벨을 찾아 커맨드 인덱스를 리턴한다.
        /// </summary>
        /// <param name="labelName">라벨 명</param>
        /// <returns>라벨 커맨드 인덱스. 찾지 못하면 무효값</returns>
        private int FindLabelCommandIndex(string labelName)
        {
            int commandIdx;
            if (_labels.TryGetValue(labelName, out commandIdx))
            {
                return commandIdx;
            }
            else
            {
                return Define._invalidLabelIndex;
            }
        }

        /// <summary>
        /// 라벨을 찾아 다음에 실행할 커맨드로 지정한다.
        /// </summary>
        public void JumpToLabel(string labelName)
        {
            int idx = FindLabelCommandIndex(labelName);
            if (idx == Define._invalidLabelIndex)
            {
                Debug.LogError("[ScenarioLoader.CannotFindLabel]" + labelName);
                return;
            }
            _commandIdx = idx;
        }

        public bool HasRemainedCommand()
        {
            return (_commands.Count > _commandIdx);
        }

        public void UpdateCommand()
        {
            int curIdx = _commandIdx;
            _commandIdx++;  // 커맨드 실행에 의해 인덱스가 다시 바뀔 수 있으므로 미리 증가시켜둔다.
            Command command = _commands[curIdx];    // 커맨드 목록이 바뀔 수 있으므로 별도 변수에 담아둔다.
            command.Do();
        }
    }
}