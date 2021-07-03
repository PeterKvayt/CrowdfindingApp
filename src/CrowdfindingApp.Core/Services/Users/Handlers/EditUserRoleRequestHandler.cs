using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Core.Handlers;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Users;
using AutoMapper;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Core.Validators;
using System.Collections.Generic;
using System.Linq;
using CrowdfindingApp.Common.Core.DataTransfers.Users;

namespace CrowdfindingApp.Core.Services.Users.Handlers
{
    public class EditUserRoleRequestHandler : RequestHandlerBase<EditUserRoleRequestMessage, ReplyMessageBase,User>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public EditUserRoleRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<(ReplyMessageBase, User)> ValidateRequestMessageAsync(EditUserRoleRequestMessage requestMessage)
        {
            var (reply, user) = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new IdValidator();
            await reply.MergeAsync(await validator.ValidateAsync(requestMessage.UserId));

            user = await _userRepository.GetByIdAsync(new Guid(requestMessage.UserId));
            if(user == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, user);
            }

            if(!_roles.ContainsKey(requestMessage.RoleName))
            {
                reply.AddObjectNotFoundError();
                return (reply, user);
            }

            return (reply, user);
        }

        private readonly Dictionary<string, Guid> _roles = new Dictionary<string, Guid>
        {
            { nameof(Common.Immutable.Roles.Admin), new Guid(Common.Immutable.Roles.Admin) },
            { nameof(Common.Immutable.Roles.DefaultUser), new Guid(Common.Immutable.Roles.DefaultUser) },
        };

        protected override async Task<ReplyMessageBase> ExecuteAsync(EditUserRoleRequestMessage request, User userForUpdate)
        {
            //var updates = _mapper.Map<User>(userForUpdate);
            var newRole = _roles.First(x => x.Key == request.RoleName);
            userForUpdate.RoleId = newRole.Value;
            await _userRepository.UpdateAsync(userForUpdate, _mapper);

            return new ReplyMessageBase();
        }
    }
}
