using UnityEngine;
using System.Text.RegularExpressions;

namespace Game
{
    /// <summary>
    /// 프레젠테이션 생성
    /// </summary>
    public class Command_Presentation : Command
    {
        public const string _ID = "pt";

        public override void Do()
        {
            if (GameSystem._Instance._Presentation != null)
            {
                Debug.LogError("[Command_Presentation.Do.AlreadyCreated]");
                return;
            }

            var prefab = Resources.Load<Presentation>(Define._systemRoot + "/Presentation");
            if (prefab == null)
            {
                Debug.LogError("[Command_Presentation.Do.CannotLoadPrefab]");
                return;
            }

            var presentation = Object.Instantiate(prefab);
            GameSystem._Instance._Presentation = presentation;
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

    /// <summary>
    /// 프레젠테이션 지우기
    /// </summary>
    public class Command_RemovePresentation : Command
    {
        public const string _ID = "removept";

        public override void Do()
        {
            if (GameSystem._Instance._Presentation == null)
            {
                Debug.LogError("[Command_RemovePresentation.Do.AlreadyRemoved]");
                return;
            }

            GameObject.Destroy(GameSystem._Instance._Presentation.gameObject);
            GameSystem._Instance._Presentation = null;
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
}