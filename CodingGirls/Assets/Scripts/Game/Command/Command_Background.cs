using UnityEngine;
using System.Text.RegularExpressions;

namespace Game
{
    public class Command_LoadBackground : Command
    {
        public const string _ID = "loadbg";
        public string _Name { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._Background.LoadTexture(_Name);
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
    }

    public class Command_Background : Command
    {
        public const string _ID = "bg";
        public string _Name { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._Background.SetTexture(_Name);
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
    }

    public class Command_BackgroundColor : Command
    {
        public const string _ID = "bgcolor";
        public Color _Color { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._Background.SetColor(_Color);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._colorGroup + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Color = StringDefine.ParseColor(groups[1].Value);
        }
    }
}