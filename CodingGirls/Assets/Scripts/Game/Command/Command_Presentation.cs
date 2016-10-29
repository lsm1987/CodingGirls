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

            var prefab = Resources.Load<Presentation>(Define._presentationRoot + "/Presentation");
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
                Debug.LogError("[Command_RemovePresentation.Do.PtNotExist]");
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

    /// <summary>
    /// 프레젠테이션 레이아웃 지정
    /// <para>레아이아웃 명 미지정 시 모두 비활성화</para>
    /// </summary>
    public class Command_PresentationLayout : Command
    {
        public const string _ID = "ptlayout";
        public string _LayoutName { get; private set; }

        public override void Do()
        {
            if (GameSystem._Instance._Presentation == null)
            {
                Debug.LogError("[Command_PresentationLayout.Do.PtNotExist]");
                return;
            }

            if (_LayoutName == string.Empty)
            {
                GameSystem._Instance._Presentation.ClearLayout();
            }
            else
            {
                GameSystem._Instance._Presentation.SetLayout(_LayoutName);
            }
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^(?:" + StringDefine.Pattern._wordGroup + ")?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _LayoutName = groups[1].Value;
        }
    }
}