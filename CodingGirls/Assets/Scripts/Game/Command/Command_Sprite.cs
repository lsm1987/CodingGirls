using UnityEngine;
using System.Text.RegularExpressions;

namespace Game
{
    public class Command_Sprite : Command
    {
        public const string _ID = "spr";
        public string _Name { get; private set; }
        public Vector2 _Position { get; private set; }
        public float _Scale { get; private set; }

        public override void Do()
        {
            Sprite spr = GameSystem._Instance._SpriteManager.GetOrCreate(_Name);
            if (spr == null)
            {
                Debug.LogError("[Command_Sprite.Do.InvalidName]");
                return;
            }

            spr.SetPosition(_Position);
            spr.SetScale(_Scale);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._vector2Group
                    + " " + StringDefine.Pattern._floatGroup
                    + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Name = groups[1].Value;
            _Position = StringDefine.ParseVector2(groups[2].Value);
            _Scale = StringDefine.ParseFloat(groups[3].Value);
        }
    }

    public class Command_RemoveSprite : Command
    {
        public const string _ID = "removespr";
        public string _Name { get; private set; }

        public override void Do()
        {
            if (_Name == string.Empty)
            {
                GameSystem._Instance._SpriteManager.Remove();
            }
            else
            {
                GameSystem._Instance._SpriteManager.Remove(_Name);
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
            _Name = groups[1].Value;
        }
    }
}