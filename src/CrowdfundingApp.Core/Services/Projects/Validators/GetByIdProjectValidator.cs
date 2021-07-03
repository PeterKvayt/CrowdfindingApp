using System;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Immutable;
using CrowdfundingApp.Common.Core.Messages.Projects;
using FluentValidation;

namespace CrowdfundingApp.Core.Services.Projects.Validators
{
    public class GetByIdProjectValidator : AbstractValidator<GetProjectByIdRequestMessage>
    {
        public GetByIdProjectValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.ProjectId).NotEmpty().WithErrorCode(CommonErrorMessageKeys.EmptyId);
            RuleFor(x => x.ProjectId).Must(x => Guid.TryParse(x, out var _))
                .When(x => x.ProjectId.NonNullOrWhiteSpace())
                .WithErrorCode(CommonErrorMessageKeys.EmptyId);
        }
    }
}
