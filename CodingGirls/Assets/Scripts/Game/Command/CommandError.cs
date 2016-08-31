namespace Game
{
    /// <summary>
    /// 커맨드에서 발생시킨 오류 정보
    /// </summary>
    public class CommandError
    {
        private string _message;
        public CommandError _InnerError { get; private set; }

        public CommandError()
        {
        }

        public CommandError(string message)
        {
            _message = message;
        }

        public CommandError(string message, CommandError inner)
        {
            _message = message;
            _InnerError = inner;
        }

        public override string ToString()
        {
            return _message;
        }
    }

    /// <summary>
    /// 유효하지 않은 커맨드 ID 에러
    /// </summary>
    public class CommandInvalidIDError : CommandError
    {
        private const string _messageHeader = "Invalid command ID: ";

        public CommandInvalidIDError()
            : base()
        {
        }

        public CommandInvalidIDError(string id)
            : base(_messageHeader + id)
        {
        }

        public CommandInvalidIDError(string id, CommandError inner)
            : base(_messageHeader + id, inner)
        {
        }
    }

    /// <summary>
    /// 패턴 일치하지 않음 에러
    /// </summary>
    public class CommandInvalidPatternError : CommandError
    {
        private const string _messageHeader = "Invalid parameter pattern: ";

        public CommandInvalidPatternError()
            : base()
        {
        }

        public CommandInvalidPatternError(string pattern)
            : base(_messageHeader + pattern)
        {
        }

        public CommandInvalidPatternError(string pattern, CommandError inner)
            : base(_messageHeader + pattern, inner)
        {
        }
    }
}