using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Maintainers.Hasher;
using CrowdfindingApp.Common.Maintainers.TokenManager;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Users;
using CrowdfindingApp.Core.Services.Users.Validators;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Users.Handlers
{
    public class ResetPasswordRequestHandler : RequestHandlerBase<ResetPasswordRequestMessage, ReplyMessageBase, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly ITokenManager _tokenManager;

        public ResetPasswordRequestHandler(IUserRepository userRepository, ITokenManager tokenManager, IHasher hasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
        }

        protected override async Task<(ReplyMessageBase, User)> ValidateRequestMessageAsync(ResetPasswordRequestMessage requestMessage)
        {
            var (reply, operationСontext) = await base.ValidateRequestMessageAsync(requestMessage);

            Guid userId;
            if(requestMessage.Token.IsNullOrEmpty() || !_tokenManager.ValidateResetPasswordToken(requestMessage.Token, out userId))
            {
                reply.AddValidationError(UserErrorKeys.InvalidToken);
                return (reply, operationСontext);
            }

            operationСontext = await _userRepository.GetByIdAsync(userId);
            if(operationСontext == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, operationСontext);
            }
            var validator = new PasswordValidator();
            var validationResult = await validator.ValidateAsync(requestMessage.Password);
            if(!validationResult.IsValid)
            {
                await reply.MergeAsync(validationResult);
                return (reply, operationСontext);
            }

            if(!validator.Confirm(requestMessage.Password, requestMessage.ConfirmPassword))
            {
                reply.AddValidationError(UserErrorKeys.PasswordConfirmationFail);
                return (reply, operationСontext);
            }

            return (reply, operationСontext);
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(ResetPasswordRequestMessage request, User user)
        {
            var reply = await PreExecuteAsync(request);

            var (passwordHash, salt) = _hasher.GetHashWithSalt(request.Password);

            await _userRepository.UpdatePasswordAsync(user.Id, passwordHash, salt);

            return reply;
        }
    }
}
