using System;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Core.Messages.Projects;
using FluentValidation;

namespace CrowdfindingApp.Core.Services.Projects.Validators
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
