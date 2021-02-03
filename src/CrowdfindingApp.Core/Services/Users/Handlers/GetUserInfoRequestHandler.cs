using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Users;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Users;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Users.Handlers
{
    public class GetUserInfoRequestHandler : NullOperationContextRequestHandler<GetUserInfoRequestMessage, ReplyMessage<UserInfo>>
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

        protected override async Task<ReplyMessage<UserInfo>> ExecuteAsync(GetUserInfoRequestMessage request)
        {
            var reply = new ReplyMessage<UserInfo>();
            var user = await _userRepository.GetUserByIdAsync(User.GetUserId());
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
