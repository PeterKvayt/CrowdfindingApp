using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Maintainers.EmailSender;
using CrowdfundingApp.Common.Core.Maintainers.Hasher;
using CrowdfundingApp.Common.Core.Maintainers.TokenManager;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Users;
using CrowdfundingApp.Core.Services.Users.Validators;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Core.Extensions;

namespace CrowdfundingApp.Core.Services.Users.Handlers
{
    public class RegisterRequestHandler : NullOperationContextRequestHandler<RegisterRequestMessage, ReplyMessageBase>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly ITokenManager _tokenManager;
        private readonly IEmailSender _emailSender;

        public RegisterRequestHandler(IUserRepository userRepository, IHasher hasher, ITokenManager tokenManager, IEmailSender emailSender)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(RegisterRequestMessage requestMessage)
        {
            var reply = new ReplyMessageBase();

            if(requestMessage.Email.IsNullOrEmpty())
            {
                return reply.AddValidationError(UserErrorKeys.EmptyEmail);
            }

            if(requestMessage.Password.IsNullOrEmpty())
            {
                return reply.AddValidationError(UserErrorKeys.EmptyPassword);
            }

            var validator = new PasswordValidator();
            if(!validator.Confirm(requestMessage.Password, requestMessage.ConfirmPassword))
            {
                return reply.AddValidationError(UserErrorKeys.PasswordConfirmationFail);
            }

            var validationResult = await validator.ValidateAsync(requestMessage.Password);
            if(!validationResult.IsValid)
            {
                return await reply.MergeAsync(validationResult);
            }

            var userWithEmail = await _userRepository.GetUsersAsync(new UserFilter { Email = new List<string> { requestMessage.Email } });
            if(userWithEmail.Any())
            {
                return reply.AddValidationError(key: UserErrorKeys.UniqueEmail, parameters: requestMessage.Email);
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(RegisterRequestMessage request)
        {
            var (passwordHash, salt) = _hasher.GetHashWithSalt(request.Password);
            var user = new User()
            {
                Id = new Guid(),
                Email = request.Email.ToUpperInvariant(),
                PasswordHash = passwordHash,
                Salt = salt,
                CreatedDateTime = DateTime.UtcNow,
                RoleId = new Guid(Common.Immutable.Roles.DefaultUser),
                Active = true,
                UserName = request.Email.ToUpperInvariant(),
                EmailConfirmed = false
            };

            // ToDo: fix email sending.
            //var emailConfirmationToken = _tokenManager.GetConfirmEmailToken(user.Id);
            //await _emailSender.SendEmailConfirmationAsync(user.Email, emailConfirmationToken);

            await _userRepository.AddAsync(user);

            return new ReplyMessageBase(); 
        }
    }
}
