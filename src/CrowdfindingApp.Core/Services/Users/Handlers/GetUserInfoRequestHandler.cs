using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.Core.DataTransfers.Users;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Core.Handlers;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Users;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Core.Services.Users.Handlers
{
    public class GetUserInfoRequestHandler : NullOperationContextRequestHandler<GetUserInfoRequestMessage, ReplyMessage<UserInfo>>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IRoleRepository _roleRepository;
        private IConfiguration _configuration;


        public GetUserInfoRequestHandler(IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override async Task<ReplyMessage<UserInfo>> ExecuteAsync(GetUserInfoRequestMessage request)
        {
            var reply = new ReplyMessage<UserInfo>();
            var user = await _userRepository.GetByIdAsync(User.GetUserId());
            user.Image = GetImageUrl(user.Id, user.Image);
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

        private string GetImageUrl(Guid userId, string image)
        {
            if(image.IsNullOrWhiteSpace())
            {
                return null;
            }
            return $"{_configuration["FileStorageConfiguration:PermanentFolderName"]}/Users/{userId}/{image}";
        }
    }
}
