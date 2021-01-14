using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using CrowdfindingApp.Common.Messages.User;
using AutoMapper;
using CrowdfindingApp.Common.Extensions;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequestMessage, ReplyMessageBase, Models.User>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UpdateUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<(ReplyMessageBase, Models.User)> ValidateRequestMessageAsync(UpdateUserRequestMessage requestMessage)
        {
            var (reply, user) = await base.ValidateRequestMessageAsync(requestMessage);

            if(requestMessage.Email.IsNullOrEmpty())
            {
                reply.AddValidationError(ErrorKeys.EmptyEmail);
                return (reply, user);
            }

            if(requestMessage.Name.IsNullOrEmpty())
            {
                reply.AddValidationError(ErrorKeys.EmptyName);
                return (reply, user);
            }

            if(requestMessage.Surname.IsNullOrEmpty())
            {
                reply.AddValidationError(ErrorKeys.EmptySurname);
                return (reply, user);
            }

            user = await _userRepository.GetUserByIdAsync(User.GetUserId());
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
                    reply.AddValidationError(ErrorKeys.UniqueEmail);
                    return (reply, user);
                }
            }

            return (reply, user);
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(UpdateUserRequestMessage request, Models.User userForUpdate)
        {
            var userSnapshot = _mapper.Map<Models.User>(request);

            PrepareUser(userSnapshot, userForUpdate);

            await _userRepository.UpdateUserAsync(userForUpdate);

            return new ReplyMessageBase();
        }

        private void PrepareUser(Models.User snapshot, Models.User user)
        {
            user.UserName = snapshot.UserName;
            user.Email = snapshot.Email;
            user.Name = snapshot.Name;
            user.Surname = snapshot.Surname;
            user.MiddleName = snapshot.MiddleName;
            user.Image = snapshot.Image;
        }
    }
}
