using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.User;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class GetUserInfoRequestHandler : RequestHandlerBase<GetUserInfoRequestMessage, ReplyMessage<UserInfo>, Models.User>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IRoleRepository _roleRepository;

        public GetUserInfoRequestHandler(IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        protected override async Task<(ReplyMessageBase, Models.User)> ValidateRequestMessageAsync(GetUserInfoRequestMessage requestMessage)
        {
            var (reply, user) = await base.ValidateRequestMessageAsync(requestMessage);

            if(!Guid.TryParse(requestMessage.Id, out Guid userId))
            {
                reply.AddValidationError(ErrorKeys.InvalidUserId);
                return (reply, user);
            }

            user = await _userRepository.GetUserByIdAsync(userId);
            if(user == null)
            {
                reply.AddValidationError(ErrorKeys.InvalidUserId);
                return (reply, user);
            }

            return (reply, user);
        }

        protected override async Task<ReplyMessage<UserInfo>> ExecuteAsync(GetUserInfoRequestMessage request, Models.User user)
        {
            var reply = new ReplyMessage<UserInfo>();

            var roles = await _roleRepository.GetNamesAsync();
            var roleNames = roles.ToDictionary(x => x.Key.ToString(), x => x.Value);

            reply.Value = _mapper.Map<UserInfo>(user, x => SetRoleOptions(roleNames, x.Items));

            return reply;
        }

        private void SetRoleOptions(Dictionary<string, string> roles, IDictionary<string, object> options)
        {
            foreach(var role in roles)
            {
                options[role.Key] = role.Value;
            }
        }
    }
}
