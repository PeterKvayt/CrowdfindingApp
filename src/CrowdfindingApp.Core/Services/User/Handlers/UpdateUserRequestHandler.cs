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
    public class UpdateUserRequestHandler : NullOperationContextRequestHandler<UpdateUserRequestMessage, ReplyMessageBase>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UpdateUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(UpdateUserRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);

            if(requestMessage.Id.IsNullOrEmpty())
            {
                reply.AddValidationError(ErrorKeys.UserIdIsNullOrEmpty);
                return reply;
            }

            if(!Guid.TryParse(requestMessage.Id, out Guid guid))
            {
                reply.AddValidationError(ErrorKeys.InvalidUserId);
                return reply;
            }

            return reply;
        }

        protected override async Task<ReplyMessageBase> ExecuteAsync(UpdateUserRequestMessage request)
        {
            var userSnapshot = _mapper.Map<Models.User>(request);

            var userForUpdate = await _userRepository.GetUserByIdAsync(userSnapshot.Id);
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
