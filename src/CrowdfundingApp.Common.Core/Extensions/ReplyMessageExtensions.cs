using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.Messages;
using FluentValidation.Results;

namespace CrowdfundingApp.Common.Core.Extensions
{
    public static class ReplyMessageExtensions
    {
        public static T Merge<T>(this T message, ValidationResult validationResult) where T : ReplyMessageBase
        {
            if(validationResult.IsValid)
            {
                return message;
            }

            foreach(var error in validationResult.Errors)
            {
                message.AddValidationError(error.ErrorCode);
            }

            return message;
        }

        public static async Task<T> MergeAsync<T>(this T message, ValidationResult validationResults) where T : ReplyMessageBase
        {
            if(validationResults.IsValid)
            {
                return message;
            }

            foreach(var error in validationResults.Errors)
            {
                var parametersTask = error.CustomState as Task<string[]>;
                var parameterTask = error.CustomState as Task<string>;
                if(parametersTask is null && parameterTask is  null)
                {
                    message.AddValidationError(error.ErrorCode, null, error.AttemptedValue);
                }
                else if(parametersTask is null)
                {
                    message.AddValidationError(error.ErrorCode, null, await parameterTask);
                }
                else
                {
                    message.AddValidationError(error.ErrorCode, null, await parametersTask);
                }
            }

            return message;
        }
    }

    
}
