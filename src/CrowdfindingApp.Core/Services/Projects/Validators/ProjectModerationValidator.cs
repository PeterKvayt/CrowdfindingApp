using System.Linq;
using CrowdfindingApp.Common.DataTransfers.Projects;
using Keys = CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys.ProjectValidationErrorKeys;
using FluentValidation;
using System;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Immutable;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrowdfindingApp.Core.Services.Projects.Validators
{
    public class ProjectModerationValidator : AbstractValidator<ProjectInfo>
    {
        public ProjectModerationValidator()
        {
            RuleFor(x => x.Id).Must(x => Guid.TryParse(x, out var _))
                .When(x => x.Id.NonNullOrWhiteSpace())
                .WithErrorCode(CommonErrorMessageKeys.InvalidIdFormat)
                .WithCustomMessageParameters(x => Task.FromResult(x.Id));

            RuleFor(x => x.CategoryId).NotEmpty().WithErrorCode(Keys.MissingCategory);
            RuleFor(x => x.CategoryId).Must(x => Guid.TryParse(x, out var _))
                .When(x => x.CategoryId.NonNullOrWhiteSpace())
                .WithErrorCode(CommonErrorMessageKeys.WrongIdFormat);

            RuleFor(x => x.Title).NotEmpty().WithErrorCode(Keys.MissingTitle);

            RuleFor(x => x.ShortDescription).NotEmpty().WithErrorCode(Keys.MissingShortDescription);
            RuleFor(x => x.ShortDescription).Must(x => x.Length <= 280)
                .When(x => x.ShortDescription.NonNullOrWhiteSpace())
                .WithErrorCode(Keys.ToLargeShortdescriptionLength)
                .WithCustomMessageParameters(x => Task.FromResult(new string[] { "280" }));

            RuleFor(x => x.FullDescription).NotEmpty().WithErrorCode(Keys.MissingFullDescription);

            RuleFor(x => x.Location).NotEmpty().WithErrorCode(Keys.MissingLocation);
            RuleFor(x => x.Location).Must(x => Guid.TryParse(x, out var _))
                .When(x => x.Location.NonNullOrWhiteSpace())
                .WithErrorCode(CommonErrorMessageKeys.WrongIdFormat);

            //RuleFor(x => x.VideoUrl).NotEmpty();

            //RuleFor(x => x.Image).NotEmpty();

            RuleFor(x => x.Duration).NotEmpty().WithErrorCode(Keys.MissingDuration);
            RuleFor(x => x.Duration).Must(x => x > 0)
                .When(x => x.Duration.HasValue)
                .WithErrorCode(Keys.DurationLessThanZero);

            RuleFor(x => x.Budget).NotEmpty().WithErrorCode(Keys.MissingBudget);

            RuleFor(x => x.AuthorSurname).NotEmpty().WithErrorCode(Keys.MissingAuthorSurname);

            RuleFor(x => x.AuthorName).NotEmpty().WithErrorCode(Keys.MissingAuthorName);

            RuleFor(x => x.AuthorDateOfBirth).NotEmpty().WithErrorCode(Keys.MissingAuthorDateOfBirth);

            RuleFor(x => x.AuthorMiddleName).NotEmpty().WithErrorCode(Keys.MissingAuthorMiddleName);

            RuleFor(x => x.AuthorPersonalNo).NotEmpty().WithErrorCode(Keys.MissingAuthorPersonalNumber);
            RuleFor(x => x.AuthorPersonalNo).Matches(RegexPatterns.PassportNo)
                .When(x => x.AuthorPersonalNo.NonNullOrWhiteSpace())
                .WithErrorCode(Keys.InvalidPassportNo)
                .WithCustomMessageParameters(x => Task.FromResult(x.AuthorPersonalNo));

            RuleFor(x => x.AuthorIdentificationNo).NotEmpty().WithErrorCode(Keys.MissingAuthorPersonalNumber);
            RuleFor(x => x.AuthorIdentificationNo).Matches(RegexPatterns.IdentificationPassportNo)
                .When(x => x.AuthorIdentificationNo.NonNullOrWhiteSpace())
                .WithErrorCode(Keys.InvalidIdentificationPassportNo)
                .WithCustomMessageParameters(x => Task.FromResult(x.AuthorIdentificationNo));

            RuleFor(x => x.WhomGivenDocument).NotEmpty().WithErrorCode(Keys.MissingWhomGivenDocument);

            RuleFor(x => x.WhenGivenDocument).NotEmpty().WithErrorCode(Keys.MissingWhenGivenDocument);

            RuleFor(x => x.AuthorAddress).NotEmpty().WithErrorCode(Keys.MissingAuthorAddress);

            RuleFor(x => x.AuthorPhone).NotEmpty().WithErrorCode(Keys.MissingAuthorPhone);
            RuleFor(x => x.AuthorPhone).Matches(RegexPatterns.PhoneNumber)
                .When(x => x.AuthorPhone.NonNullOrWhiteSpace())
                .WithErrorCode(Keys.InvalidPhoneNumber)
                .WithCustomMessageParameters(x => Task.FromResult(x.AuthorPhone));

            RuleFor(x => x.Rewards).NotNull().Must(x => x.Any())
                .WithErrorCode(Keys.MissingRewards);
            RuleForEach(x => x.Rewards).SetValidator(new RewardValidator())
                .When(x => x.Rewards.Any());

            RuleFor(x => x.Questions).NotNull().Must(x => x.Any())
                .WithErrorCode(Keys.MissingQuestions);
            RuleForEach(x => x.Questions).SetValidator(new QuestionValidator())
                .When(x => x.Questions.Any());
        }
    }
}
