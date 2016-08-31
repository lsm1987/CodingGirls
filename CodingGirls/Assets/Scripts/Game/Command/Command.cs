using System.Text.RegularExpressions;

namespace Game
{
    public static class CommandUtil
    {
        public const string _idFieldName = "_ID";
    }

    public abstract class Command
    {
        public abstract void Do();

        protected abstract string _ParsePattern
        {
            get;
        }

        public void Parse(string param, out CommandError error)
        {
            CommandError matchError = null;
            Match match = Match(param, out matchError);
            if (matchError != null)
            {
                error = matchError;
                return;
            }

            CommandError setValueError = null;
            SetValue(match.Groups, out setValueError);
            if (setValueError != null)
            {
                error = setValueError;
                return;
            }

            error = null;
        }

        private Match Match(string param, out CommandError error)
        {
            Match match = Regex.Match(param, _ParsePattern);
            if (!match.Success)
            {
                error = new CommandInvalidPatternError(_ParsePattern);
                return null;
            }
            else
            {
                error = null;
                return match;
            }
        }

        protected abstract void SetValue(GroupCollection groups, out CommandError error);

        public virtual void OnParsed(out CommandError error)
        {
            error = null;
        }
    }
}