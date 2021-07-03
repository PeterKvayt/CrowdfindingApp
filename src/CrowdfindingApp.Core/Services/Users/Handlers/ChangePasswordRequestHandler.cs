using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Maintainers.Hasher;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Users;
using CrowdfindingApp.Core.Services.Users.Validators;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Users.Handlers
{
    public class ChangePasswordRequestHandler : NullOperationContextRequestHandler<ChangePasswordRequestMessage, ReplyMessageBase>
    {
        private IUserRepository _userRepository;
        private IHasher _hasher;

        public ChangePasswordRequestHandler(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(ChangePasswordRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);

            var user = await _userRepository.GetByIdAsync(User.GetUserId());
            if(user == null)
            {
                return reply.AddObjectNotFoundError();
            }

            if(!_hasher.Equals(user.PasswordHash, requestMessage.OldPassword, user.Salt))
            {
                return reply.AddSecurityError();
            }

            var validator = new PasswordValidator();
            var validationResult = await validator.ValidateAsync(requestMessage.NewPassword);
            if(!validationResult.IsValid)
            {
                return await reply.MergeAsync(validationResult);
            }

            if(!validator.Confirm(requestMessage.NewPassword, requestMessage.ConfirmPassword))
            {
                return reply.AddValidationError(UserErrorKeys.PasswordConfirmationFail);
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(ChangePasswordRequestMessage request)
        {
            var (newPasswordHash, newSalt) = _hasher.GetHashWithSalt(request.NewPassword);

            await _userRepository.UpdatePasswordAsync(User.GetUserId(), newPasswordHash, newSalt);

            return new ReplyMessageBase();
        }
    }
}
