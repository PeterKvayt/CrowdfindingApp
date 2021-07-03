using System;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Maintainers.EmailSender;
using CrowdfundingApp.Common.Core.Maintainers.Hasher;
using CrowdfundingApp.Common.Core.Maintainers.TokenManager;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Users;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfundingApp.Core.Services.Users.Handlers
{
    public class ForgotPasswordRequestHandler : NullOperationContextRequestHandler<ForgotPasswordRequestMessage, ReplyMessageBase>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly ITokenManager _tokenManager;

        public ForgotPasswordRequestHandler(IEmailSender emailSender, IUserRepository userRepository, ITokenManager tokenManager, IHasher hasher)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(tokenManager));
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(ForgotPasswordRequestMessage request)
        {
            var reply = await PreExecuteAsync(request);

            if(request.Email.IsNullOrEmpty())
            {
                return reply;
            }

            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if(user == null)
            {
                return reply;
            }
            var newPassword = Guid.NewGuid();
            var (passwordHash, salt) = _hasher.GetHashWithSalt(newPassword.ToString());

            await _userRepository.UpdatePasswordAsync(user.Id, passwordHash, salt);

            //var resetPasswordToken = _tokenManager.GetResetPasswordToken(user.Id);

            await _emailSender.SendResettedPasswordAsync(user.Email, newPassword.ToString());

            return reply;
        }
    }
}
