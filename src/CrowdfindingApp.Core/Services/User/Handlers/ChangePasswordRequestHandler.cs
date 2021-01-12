using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Maintainers.Hasher;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Services.User.Helpers;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class ChangePasswordRequestHandler : NullOperationContextRequestHandler<ChangePasswordRequestMessage, ReplyMessageBase>
    {
        private IUserRepository _userRepository;
        private IHasher _hasher;
        private PasswordValidator _passwordValidator;

        public ChangePasswordRequestHandler(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _passwordValidator = new PasswordValidator();
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(ChangePasswordRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);

            if(requestMessage.Id.IsNullOrEmpty())
            {
                return reply.AddValidationError(ErrorKeys.UserIdIsNullOrEmpty);
            }

            if(!Guid.TryParse(requestMessage.Id, out Guid guid))
            {
                return reply.AddValidationError(ErrorKeys.InvalidUserId);
            }

            var user = await _userRepository.GetUserByIdAsync(guid);
            if(user == null)
            {
                return reply.AddObjectNotFoundError();
            }

            if(!_hasher.Equals(user.PasswordHash, requestMessage.OldPassword, user.Salt))
            {
                return reply.AddSecurityError();
            }

            if(!_passwordValidator.Validate(requestMessage.NewPassword))
            {
                return reply.AddValidationError(ErrorKeys.InvalidPasswordLength);
            }

            if(!_passwordValidator.Confirm(requestMessage.NewPassword, requestMessage.ConfirmPassword))
            {
                return reply.AddValidationError(ErrorKeys.PasswordConfirmationFail);
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(ChangePasswordRequestMessage request)
        {
            var (newPasswordHash, newSalt) = _hasher.GetHashWithSalt(request.NewPassword);

            await _userRepository.UpdatePasswordAsync(new Guid(request.Id), newPasswordHash, newSalt);

            return new ReplyMessageBase();
        }
    }
}
