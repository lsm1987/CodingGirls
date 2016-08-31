using System.Text.RegularExpressions;

namespace Game
{
    public class Command_LoadBGM : Command
    {
        public const string _ID = "loadbgm";
        public string _Path { get; private set; }

        public override void Do()
        {
            SoundManager._Instance.LoadBGM(_Path);
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
            _Path = groups[1].Value;
        }
    }

    public class Command_BGM : Command
    {
        public const string _ID = "bgm";
        public string _Path { get; private set; }

        public override void Do()
        {
            if (_Path == null)
            {
                SoundManager._Instance.StopBGM();
            }
            else
            {
                SoundManager._Instance.PlayBGM(_Path);
            }
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
            _Path = (groups[1].Value == "") ? null : groups[1].Value;
        }
    }

    public class Command_Sound : Command
    {
        public const string _ID = "sound";
        public string _Path { get; private set; }

        public override void Do()
        {
            SoundManager._Instance.PlaySound(_Path);
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
            _Path = groups[1].Value;
        }
    }
}