using System.Text.RegularExpressions;

namespace Game
{
    public class Command_ForegroundFadeIn : Command
    {
        public const string _ID = "fg";
        public float _Duration { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._Foreground.FadeIn(_Duration);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._floatGroup + "?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Duration = StringDefine.ParseFloat(groups[1].Value);
        }
    }

    public class Command_ForegroundFadeOut : Command
    {
        public const string _ID = "fgout";
        public float _Duration { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._Foreground.FadeOut(_Duration);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._floatGroup + "?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Duration = StringDefine.ParseFloat(groups[1].Value);
        }
    }

    public class Command_ForegroundCover : Command
    {
        public const string _ID = "fgcover";
        public bool _ToLeft { get; private set; }
        public float _Duration { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._Foreground.Cover(_ToLeft, _Duration);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^(left|right)"
                    + "(?: " + StringDefine.Pattern._floatGroup + ")?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _ToLeft = (groups[1].Value == "left");
            _Duration = StringDefine.ParseFloat(groups[2].Value, Define._foregroundCoverDefaultDuration);
        }
    }

    public class Command_ForegroundSweep : Command
    {
        public const string _ID = "fgsweep";
        public bool _ToLeft { get; private set; }
        public float _Duration { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._Foreground.Sweep(_ToLeft, _Duration);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^(left|right)"
                    + "(?: " + StringDefine.Pattern._floatGroup + ")?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _ToLeft = (groups[1].Value == "left");
            _Duration = StringDefine.ParseFloat(groups[2].Value, Define._foregroundCoverDefaultDuration);
        }
    }
}