using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Maintainers.Hasher;
using CrowdfindingApp.Common.Maintainers.TokenManager;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Services.User.Helpers;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class ResetPasswordRequestHandler : RequestHandlerBase<ResetPasswordRequestMessage, ReplyMessageBase, Models.User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly ITokenManager _tokenManager;
        private readonly PasswordValidator _passwordValidator;

        public ResetPasswordRequestHandler(IUserRepository userRepository, ITokenManager tokenManager, IHasher hasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
            _passwordValidator = new PasswordValidator();
        }

        protected override async Task<(ReplyMessageBase, Models.User)> ValidateRequestMessageAsync(ResetPasswordRequestMessage requestMessage)
        {
            var (reply, operationСontext) = await base.ValidateRequestMessageAsync(requestMessage);

            Guid userId;
            if(requestMessage.Token.IsNullOrEmpty() || !_tokenManager.ValidateResetPasswordToken(requestMessage.Token, out userId))
            {
                reply.AddValidationError(ErrorKeys.InvalidToken);
                return (reply, operationСontext);
            }

            operationСontext = await _userRepository.GetUserByIdAsync(userId);
            if(operationСontext == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, operationСontext);
            }

            if(!_passwordValidator.Validate(requestMessage.Password))
            {
                reply.AddValidationError(ErrorKeys.InvalidPassword);
                return (reply, operationСontext);
            }

            if(!_passwordValidator.Confirm(requestMessage.Password, requestMessage.ConfirmPassword))
            {
                reply.AddValidationError(ErrorKeys.PasswordConfirmationFail);
                return (reply, operationСontext);
            }

            return (reply, operationСontext);
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(ResetPasswordRequestMessage request, Models.User user)
        {
            var reply = await PreExecuteAsync(request);

            var (passwordHash, salt) = _hasher.GetHashWithSalt(request.Password);

            await _userRepository.UpdatePasswordAsync(user.Id, passwordHash, salt);

            return reply;
        }
    }
}
