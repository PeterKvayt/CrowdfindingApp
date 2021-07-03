using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Core.Messages.Users;
using FluentValidation;

namespace CrowdfindingApp.Core.Services.Users.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequestMessage>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithErrorCode(UserErrorKeys.EmptyEmail);

            //RuleFor(x => x.Name).NotEmpty()
            //    .WithErrorCode(UserErrorKeys.EmptyName);

            //RuleFor(x => x.Surname).NotEmpty()
            //    .WithErrorCode(UserErrorKeys.EmptySurname);

        }
    }
}
