using System;

namespace CrowdfindingApp.Common.Core.DataTransfers.Errors
{
    public class ErrorInfo
    {
        public ErrorInfo()
            : this(string.Empty, string.Empty)
        {
        }

        public ErrorInfo(string errorMessage)
            : this(Guid.NewGuid().ToString(), errorMessage)
        {
        }

        public ErrorInfo(string key, string message)
        {
            _key = key;
            _message = message;
        }

        public ErrorInfo(string key, string message, params object[] messageParameters)
            : this(key ?? Guid.NewGuid().ToString(), message)
        {
            _parameters = messageParameters;
        }

        private string _key;

        public virtual string Key
        {
            get => _key;
            set => _key = value;
        }

        private string _message;

        public virtual string Message
        {
            get => _message;
            set => _message = value;
        }

        private object[] _parameters;

        public virtual object[] Parameters
        {
            get => _parameters;
            set => _parameters = value;
        }

        public override string ToString()
        {
            return $"{base.ToString()}. Key: '{Key}', ErrorMessage: '{Message}'";
        }
    }
}
