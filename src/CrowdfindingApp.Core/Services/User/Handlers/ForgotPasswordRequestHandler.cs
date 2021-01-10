using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Maintainers.EmailSender;
using CrowdfindingApp.Common.Maintainers.TokenManager;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class ForgotPasswordRequestHandler : NullOperationContextRequestHandler<ForgotPasswordRequestMessage, ReplyMessageBase>
    {
        private IEmailSender _emailSender;
        private IUserRepository _userRepository;
        private ITokenManager _tokenManager;

        public ForgotPasswordRequestHandler(IEmailSender emailSender, IUserRepository userRepository, ITokenManager tokenManager)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
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

            var resetPasswordToken = _tokenManager.GetResetPasswordToken(user.Id);

            await _emailSender.SendResetPasswordUrlAsync(user.Email, resetPasswordToken);

            return reply;
        }
    }
}
