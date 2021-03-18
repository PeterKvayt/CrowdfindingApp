using CrowdfindingApp.Common.DataTransfers.Questions;
using CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys;
using FluentValidation;

namespace CrowdfindingApp.Core.Services.Projects.Validators
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
