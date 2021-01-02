using System;
using System.Collections.Generic;
using System.Linq;
using CrowdfindingApp.Common.DataTransfers.Errors;

namespace CrowdfindingApp.Common.Messages
{
    public class ReplyMessageBase : MessageBase
    {
        public static readonly string ObjectNotFoundMessageKey = "ObjectNotFoundMsg";
        public static readonly string ValidationMessageKey = "ValidationFailedMsg";
        public static readonly string SystemErrorMessageKey = "SystemErrorMsg";

        public ReplyMessageBase()
            : this(null)
        { }

        public ReplyMessageBase(ReplyMessageBase result)
        {
            Errors = result != null ? new List<ErrorInfo>(result.Errors) : new List<ErrorInfo>();
        }

        public bool Success => Errors == null || Errors?.Count == 0;

        public List<ErrorInfo> Errors { get; set; }

        public static ReplyMessageBase Empty => new ReplyMessageBase();
        public static ReplyMessageBase NotAuthorized => new ReplyMessageBase().AddNotAuthorizedError();
        public static ReplyMessageBase NoPermission => new ReplyMessageBase().AddSecurityError();
        public static ReplyMessageBase NotFound => new ReplyMessageBase().AddObjectNotFoundError();

        public ReplyMessageBase AddError(ErrorInfo errorInfo)
        {
            Errors.Add(errorInfo);
            return this;
        }

        public ReplyMessageBase AddSystemError(string key = null, string msg = null, params object[] parameters)
        {
            Errors.Add(new SystemErrorInfo(key, msg)
            {
                Parameters = parameters
            });
            return this;
        }

        public ReplyMessageBase AddSecurityError(string key = null, string msg = null, params object[] parameters)
        {
            Errors.Add(new SecurityErrorInfo(key, msg)
            {
                Parameters = parameters
            });
            return this;
        }

        public ReplyMessageBase AddValidationError(string key, string msg = null, params object[] parameters)
        {
            Errors.Add(new ValidationErrorInfo(key, msg) 
            { 
                Parameters = parameters 
            });
            return this;
        }

        public ReplyMessageBase AddObjectNotFoundError(string key = null, string msg = null, params object[] parameters)
        {
            Errors.Add(new ObjectNotFoundErrorInfo(key, msg) 
            { 
                Parameters = parameters 
            });
            return this;
        }

        public ReplyMessageBase AddNotAuthorizedError(string key = null, string msg = null, params object[] parameters)
        {
            Errors.Add(new NotAuthorizedErrorInfo(key, msg)
            {
                Parameters = parameters
            });
            return this;
        }

        public ReplyMessageBase Merge(ReplyMessageBase message)
        {
            if(message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var currentErrors = (Errors ?? new List<ErrorInfo>()).ToList();
            currentErrors.AddRange(message.Errors ?? new List<ErrorInfo>());
            Errors = currentErrors;
            return this;
        }
    }
}
