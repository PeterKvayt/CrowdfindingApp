using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.User;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Helpers;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Core.Services.User.Filters;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class RegisterRequestHandler : RequestHandlerBase<RegisterRequestMessage, ReplyMessage<RegistrationResultInfo>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;

        public RegisterRequestHandler(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessage(RegisterRequestMessage requestMessage)
        {
            var reply = new ReplyMessageBase();

            var userWithEmail = await _userRepository.GetUsersAsync(new UserFilter { Email = new List<string> { requestMessage.Email } });
            if(userWithEmail != null)
            {
                return reply.AddValidationError(ErrorKeys.UniqueEmail);
            }

            if(requestMessage.Password.Length < 2)
            {
                return reply.AddValidationError(ErrorKeys.PasswordLength);
            }

            return reply;
        }

        protected override async Task<ReplyMessage<RegistrationResultInfo>> ExecuteAsync(RegisterRequestMessage request)
        {
            var user = new Models.User()
            {
                Id = new Guid(),
                Email = request.Email.ToUpperInvariant(),
                PasswordHash = _hasher.GetHash(request.Password),
                CreatedDateTime = DateTime.UtcNow,
                RoleId = new Guid(Roles.DefaultUser),
                Active = true,
                UserName = request.Email.ToUpperInvariant(),
                EmailConfirmed = false
            };

            await _userRepository.InsertUserAsync(user);

            // ToDo: Add feature to send email comfirmation

            return new ReplyMessage<RegistrationResultInfo> 
            { 
                Value = new RegistrationResultInfo { Success = true} 
            };
        }
    }
}
