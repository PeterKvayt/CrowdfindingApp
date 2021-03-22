using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Users;
using AutoMapper;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Core.Services.Users.Handlers
{
    public class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequestMessage, ReplyMessageBase,User>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UpdateUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<(ReplyMessageBase, User)> ValidateRequestMessageAsync(UpdateUserRequestMessage requestMessage)
        {
            var (reply, user) = await base.ValidateRequestMessageAsync(requestMessage);

            if(requestMessage.Email.IsNullOrEmpty())
            {
                reply.AddValidationError(UserErrorKeys.EmptyEmail);
                return (reply, user);
            }

            if(requestMessage.Name.IsNullOrEmpty())
            {
                reply.AddValidationError(UserErrorKeys.EmptyName);
                return (reply, user);
            }

            if(requestMessage.Surname.IsNullOrEmpty())
            {
                reply.AddValidationError(UserErrorKeys.EmptySurname);
                return (reply, user);
            }

            user = await _userRepository.GetByIdAsync(User.GetUserId());
            if(user == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, user);
            }

            // ToDo: validate user name

            if(!requestMessage.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                var emailIsFree = await _userRepository.GetUserByEmailAsync(requestMessage.Email) == null;
                if(!emailIsFree)
                {
                    reply.AddValidationError(UserErrorKeys.UniqueEmail);
                    return (reply, user);
                }
            }

            return (reply, user);
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(UpdateUserRequestMessage request, User userForUpdate)
        {
            var userSnapshot = _mapper.Map<User>(request);

            await _userRepository.UpdateAsync(userSnapshot, _mapper);

            return new ReplyMessageBase();
        }
    }
}
