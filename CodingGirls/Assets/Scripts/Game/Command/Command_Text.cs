using System.Text.RegularExpressions;

namespace Game
{
    public class Command_Name : Command
    {
        public const string _ID = "name";
        public string _Name { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._UI._Dialogue.SetName(_Name);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup + "?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Name = groups[1].Value;
        }
    }

    public class Command_Text : Command
    {
        public const string _ID = "text";
        public string _Text { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._UI._Dialogue.SetText(_Text);
        }

        protected override string _ParsePattern
        {
            get
            {
                return StringDefine.Pattern._anyString;
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Text = StringDefine.ReplaceEscape(groups[0].Value);
        }
    }

    public class Command_HideText : Command
    {
        public const string _ID = "hidetext";

        public override void Do()
        {
            GameSystem._Instance._UI._Dialogue.SetActivate(false);
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