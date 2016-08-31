using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text.RegularExpressions;

namespace Game
{
    public class Command_Wait : Command
    {
        public const string _ID = "wait";
        public float _Duration { get; private set; }

        public override void Do()
        {
            GameSystem._Instance.Wait(_Duration);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._floatGroup + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Duration = StringDefine.ParseFloat(groups[1].Value);
        }
    }

    public class Command_Label : Command
    {
        public const string _ID = "label";
        public string _Name { get; private set; }

        public override void Do()
        {
            // do nothing
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Name = groups[1].Value;
        }

        public override void OnParsed(out CommandError error)
        {
            bool result = GameSystem._Instance._ScenarioManager.AddLabelOnParsed(_Name);
            if (!result)
            {
                error = new CommandError("Cannot add label:" + _Name);
                return;
            }
            error = null;
        }
    }

    public class Command_Jump : Command
    {
        public const string _ID = "jump";
        public string _LabelName { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._ScenarioManager.JumpToLabel(_LabelName);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _LabelName = groups[1].Value;
        }
    }

    public class Command_Select : Command
    {
        public const string _ID = "select";

        public override void Do()
        {
            GameSystem._Instance._UI.CreateSelect();
        }

        protected override string _ParsePattern
        {
            get
            {
                return StringDefine.Pattern._emptyString;
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
        }
    }

    public class Command_SelectItem : Command
    {
        public const string _ID = "selectitem";
        public string _LabelName { get; private set; }
        public string _Text { get; private set; }

        public override void Do()
        {
            UISelect select = GameSystem._Instance._UI._Select;
            if (select == null)
            {
                Debug.LogError("[Command_SelectItem.NotExistSelectUI]");
                return;
            }

            select.AddItem(_Text, _LabelName);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._stringGroup
                    + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _LabelName = groups[1].Value;
            _Text = groups[2].Value;
        }
    }

    public class Command_SelectEnd : Command
    {
        public const string _ID = "selectend";

        public override void Do()
        {
            GameSystem._Instance.RegisterTask(SelectWaitTask());
        }

        private IEnumerator SelectWaitTask()
        {
            while (!GameSystem._Instance._UI._Select._SelectEnded)
            {
                yield return null;
            }

            GameSystem._Instance._ScenarioManager.JumpToLabel(GameSystem._Instance._UI._Select._SelectedLabelName);
            GameSystem._Instance._UI.RemoveSelect();
        }

        protected override string _ParsePattern
        {
            get
            {
                return StringDefine.Pattern._emptyString;
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
        }
    }

    public class Command_Title : Command
    {
        public const string _ID = "title";

        public override void Do()
        {
            GameSystem._Instance.RegisterTask(LoadTitleSceneTask());
        }

        private IEnumerator LoadTitleSceneTask()
        {
            GameSystem._Instance.LoadTitleScene();
            while (true)
            {
                yield return null;
            }
        }

        protected override string _ParsePattern
        {
            get
            {
                return StringDefine.Pattern._emptyString;
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
        }
    }

    public class Command_Scenario : Command
    {
        public const string _ID = "scenario";
        public string _ScenarioName { get; private set; }
        public string _LabelName { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._ScenarioManager.Load(_ScenarioName);
            if (!string.IsNullOrEmpty(_LabelName))
            {
                GameSystem._Instance._ScenarioManager.JumpToLabel(_LabelName);
            }
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + "(?: " + StringDefine.Pattern._wordGroup + ")?"
                    + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _ScenarioName = groups[1].Value;
            _LabelName = groups[2].Value;
        }
    }
}