using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using AutoMapper;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequestMessage, ReplyMessageBase, Data.Common.Models.User>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UpdateUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<(ReplyMessageBase, Data.Common.Models.User)> ValidateRequestMessageAsync(UpdateUserRequestMessage requestMessage)
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
                    reply.AddValidationError(UserErrorKeys.UniqueEmail);
                    return (reply, user);
                }
            }

            return (reply, user);
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(UpdateUserRequestMessage request, Data.Common.Models.User userForUpdate)
        {
            var userSnapshot = _mapper.Map<Data.Common.Models.User>(request);

            PrepareUser(userSnapshot, userForUpdate);

            await _userRepository.UpdateUserAsync(userForUpdate);

            return new ReplyMessageBase();
        }

        private void PrepareUser(Data.Common.Models.User snapshot, Data.Common.Models.User user)
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
