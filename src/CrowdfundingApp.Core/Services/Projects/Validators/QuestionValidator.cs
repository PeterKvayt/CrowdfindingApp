using CrowdfundingApp.Common.Core.DataTransfers.Questions;
using CrowdfundingApp.Core.Services.Projects.ValidationErrorKeys;
using FluentValidation;

namespace CrowdfundingApp.Core.Services.Projects.Validators
{
    public class QuestionValidator : AbstractValidator<QuestionInfo>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.Answer).NotEmpty().WithErrorCode(QuestionValidationErrorKeys.MissingAnswer);
            RuleFor(x => x.Question).NotEmpty().WithErrorCode(QuestionValidationErrorKeys.MissingQuestion);
        }
    }
}
