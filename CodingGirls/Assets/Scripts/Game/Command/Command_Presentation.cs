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

    /// <summary>
    /// 프레젠테이션 텍스트 지정
    /// </summary>
    public class Command_PresentationText : Command
    {
        public const string _ID = "pttext";
        public string _TextName { get; private set; }
        public string _Text { get; private set; }

        public override void Do()
        {
            if (GameSystem._Instance._Presentation == null)
            {
                Debug.LogError("[Command_PresentationText.Do.PtNotExist]");
                return;
            }

            var text = GameSystem._Instance._Presentation.GetText(_TextName);
            if (text == null)
            {
                Debug.LogError("[Command_PresentationText.Do.PtTextNotExist]" + _TextName);
                return;
            }

            text.text = _Text;
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + "(?: " + StringDefine.Pattern._anyStringGroup + ")?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _TextName = groups[1].Value;
            _Text = StringDefine.ReplaceEscape(groups[2].Value);
        }
    }

    /// <summary>
    /// 프레젠테이션 텍스트 덧붙임
    /// </summary>
    public class Command_PresentationTextAppend : Command
    {
        public const string _ID = "pttextappend";
        public string _TextName { get; private set; }
        public string _Text { get; private set; }

        public override void Do()
        {
            if (GameSystem._Instance._Presentation == null)
            {
                Debug.LogError("[Command_PresentationTextAppend.Do.PtNotExist]");
                return;
            }

            var text = GameSystem._Instance._Presentation.GetText(_TextName);
            if (text == null)
            {
                Debug.LogError("[Command_PresentationTextAppend.Do.PtTextNotExist]" + _TextName);
                return;
            }

            text.text += _Text;
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._anyStringGroup
                    + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _TextName = groups[1].Value;
            _Text = StringDefine.ReplaceEscape(groups[2].Value);
        }
    }
}