using System;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Maintainers.Hasher;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Users;
using CrowdfundingApp.Core.Services.Users.Validators;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using CrowdfundingApp.Common.Core.Extensions;

namespace CrowdfundingApp.Core.Services.Users.Handlers
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
