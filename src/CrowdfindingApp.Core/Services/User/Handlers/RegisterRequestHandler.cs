using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Maintainers.EmailSender;
using CrowdfindingApp.Common.Maintainers.Hasher;
using CrowdfindingApp.Common.Maintainers.TokenManager;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Services.User.Filters;
using CrowdfindingApp.Core.Services.User.Helpers;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class RegisterRequestHandler : NullOperationContextRequestHandler<RegisterRequestMessage, ReplyMessageBase>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly ITokenManager _tokenManager;
        private readonly IEmailSender _emailSender;
        private readonly PasswordValidator _passwordValidator;

        public RegisterRequestHandler(IUserRepository userRepository, IHasher hasher, ITokenManager tokenManager, IEmailSender emailSender)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _passwordValidator = new PasswordValidator();
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(RegisterRequestMessage requestMessage)
        {
            var reply = new ReplyMessageBase();

            if(requestMessage.Email.IsNullOrEmpty())
            {
                return reply.AddValidationError(ErrorKeys.EmptyEmail);
            }

            if(requestMessage.Password.IsNullOrEmpty())
            {
                return reply.AddValidationError(ErrorKeys.EmptyPassword);
            }

            if(!_passwordValidator.Confirm(requestMessage.Password, requestMessage.ConfirmPassword))
            {
                return reply.AddValidationError(ErrorKeys.PasswordConfirmationFail);
            }

            var validationResult = _passwordValidator.Validate(requestMessage.Password);
            if(!validationResult.Success)
            {
                return reply.Merge(validationResult);
            }

            var userWithEmail = await _userRepository.GetUsersAsync(new UserFilter { Email = new List<string> { requestMessage.Email } });
            if(userWithEmail.Any())
            {
                return reply.AddValidationError(key: ErrorKeys.UniqueEmail, parameters: requestMessage.Email);
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(RegisterRequestMessage request)
        {
            var (passwordHash, salt) = _hasher.GetHashWithSalt(request.Password);
            var user = new Models.User()
            {
                Id = new Guid(),
                Email = request.Email.ToUpperInvariant(),
                PasswordHash = passwordHash,
                Salt = salt,
                CreatedDateTime = DateTime.UtcNow,
                RoleId = new Guid(Roles.DefaultUser),
                Active = true,
                UserName = request.Email.ToUpperInvariant(),
                EmailConfirmed = false
            };

            var emailConfirmationToken = _tokenManager.GetConfirmEmailToken(user.Id);
            await _emailSender.SendEmailConfirmationAsync(user.Email, emailConfirmationToken);

            await _userRepository.InsertUserAsync(user);

            return new ReplyMessageBase(); 
        }
    }
}
