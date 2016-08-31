using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

namespace Game
{
    public class Command_LoadModel : Command
    {
        public const string _ID = "loadmodel";
        public string _Name { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._ModelManager.LoadModel(_Name);
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

    public class Command_Model : Command
    {
        public const string _ID = "model";
        public string _Name { get; private set; }
        public string _MotionName { get; private set; }
        public bool _MotionLoop { get; private set; }
        public string _ExpressionName { get; private set; }
        public Vector3 _Position { get; private set; }
        public float _Duration { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._ModelManager.ShowModel(_Name, _MotionName, _MotionLoop, _ExpressionName, _Position, _Duration);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._booleanGroup
                    + " " + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._vector3Group
                    + " " + StringDefine.Pattern._floatGroup
                    + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Name = groups[1].Value;
            _MotionName = groups[2].Value;
            _MotionLoop = StringDefine.ParseBoolean(groups[3].Value, false);
            _ExpressionName = groups[4].Value;
            _Position = StringDefine.ParseVector3(groups[5].Value);
            _Duration = StringDefine.ParseFloat(groups[6].Value, 0.0f);
        }
    }

    public class Command_ModelHide : Command
    {
        public const string _ID = "modelhide";
        public string _Name { get; private set; }
        public float _Duration { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._ModelManager.HideModel(_Name, _Duration);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup + "(?: " + StringDefine.Pattern._floatGroup + ")?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Name = groups[1].Value;
            _Duration = StringDefine.ParseFloat(groups[2].Value);
        }
    }

    public class Command_ModelRemove : Command
    {
        public const string _ID = "modelremove";
        public string _Name { get; private set; }

        public override void Do()
        {
            GameSystem._Instance._ModelManager.RemoveModel(_Name);
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

    public class Command_Motion : Command
    {
        public const string _ID = "motion";
        public string _ModelName { get; private set; }
        public string _MotionName { get; private set; }
        public bool _MotionLoop { get; private set; }

        public override void Do()
        {
            L2DModel model = GameSystem._Instance._ModelManager.GetActiveModel(_ModelName);
            if (model == null)
            {
                Debug.LogError("[Command_Motion.NotExistActiveModel]" + _ModelName);
                return;
            }
            model.SetMotion(_MotionName, _MotionLoop, false);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._wordGroup
                    + "(?: " + StringDefine.Pattern._booleanGroup + ")?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _ModelName = groups[1].Value;
            _MotionName = groups[2].Value;
            _MotionLoop = StringDefine.ParseBoolean(groups[3].Value, false);
        }
    }

    public class Command_Expression : Command
    {
        public const string _ID = "expression";
        public string _ModelName { get; private set; }
        public string _ExpressionName { get; private set; }

        public override void Do()
        {
            L2DModel model = GameSystem._Instance._ModelManager.GetActiveModel(_ModelName);
            if (model == null)
            {
                Debug.LogError("[Command_Expression.NotExistActiveModel]" + _ModelName);
                return;
            }

            if (_ExpressionName == "")
            {
                model.ClearExpression(false);
            }
            else
            {
                model.SetExpression(_ExpressionName, false);
            }
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + "(?: " + StringDefine.Pattern._wordGroup + ")?$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _ModelName = groups[1].Value;
            _ExpressionName = groups[2].Value;
        }
    }

    public class Command_EyeBlink : Command
    {
        public const string _ID = "eyeblink";
        public string _ModelName { get; private set; }
        public bool _Enable { get; private set; }

        public override void Do()
        {
            L2DModel model = GameSystem._Instance._ModelManager.GetActiveModel(_ModelName);
            if (model == null)
            {
                Debug.LogError("[Command_EyeBlink.NotExistActiveModel]" + _ModelName);
                return;
            }

            model.SetEyeBlinkEnabled(_Enable);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._booleanGroup + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _ModelName = groups[1].Value;
            _Enable = StringDefine.ParseBoolean(groups[2].Value, false);
        }
    }

    public class Command_ModelPosition : Command
    {
        public const string _ID = "modelpos";
        public string _Name { get; private set; }
        public Vector3 _Position { get; private set; }
        public float _Duration { get; private set; }

        public override void Do()
        {
            L2DModel model = GameSystem._Instance._ModelManager.GetActiveModel(_Name);
            if (model == null)
            {
                Debug.LogError("modelpos/NoModel/" + _Name);
                return;
            }

            GameSystem._Instance.RegisterTask(ModelPositionTask(model));
        }

        private IEnumerator ModelPositionTask(L2DModel model)
        {
            model.SetPosition(_Position);
            float startTime = Time.time;
            while (Time.time - (startTime + _Duration) < 0.0f)
            {
                float rate = (Time.time - startTime) / _Duration;
                model.SetAlpha(Mathf.Lerp(0.0f, 1.0f, rate));
                yield return null;
            }
            model.SetAlpha(1.0f);
        }

        protected override string _ParsePattern
        {
            get
            {
                return "^" + StringDefine.Pattern._wordGroup
                    + " " + StringDefine.Pattern._vector3Group
                    + " " + StringDefine.Pattern._floatGroup
                    + "$";
            }
        }

        protected override void SetValue(GroupCollection groups, out CommandError error)
        {
            error = null;
            _Name = groups[1].Value;
            _Position = StringDefine.ParseVector3(groups[2].Value);
            _Duration = StringDefine.ParseFloat(groups[3].Value, 0.0f);
        }
    }
}