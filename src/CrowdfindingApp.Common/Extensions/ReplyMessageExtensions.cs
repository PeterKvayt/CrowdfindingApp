using System.Threading.Tasks;
using CrowdfindingApp.Common.Messages;
using FluentValidation.Results;

namespace CrowdfindingApp.Common.Extensions
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
                if(parametersTask != null)
                {
                    message.AddValidationError(error.ErrorCode, null, await parametersTask);
                }
                else
                {
                    message.AddValidationError(error.ErrorCode, null, error.AttemptedValue);
                }
            }
            return message;
        }
    }

    
}
