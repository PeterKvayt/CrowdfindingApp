using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.User;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Helpers;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Interfaces.Data.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace CrowdfindingApp.Core.Services.User.Handlers
{
    public class GetTokenRequestHandler : RequestHandlerBase<GetTokenRequestMessage, ReplyMessage<TokenInfo>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHasher _hasher;

        public GetTokenRequestHandler(IUserRepository userRepository, IRoleRepository roleRepository, IHasher hasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(GetTokenRequestMessage requestMessage)
        {
            // ToDo: implement operation context
            var reply = await base.ValidateRequestMessageAsync(requestMessage);

            var user = await _userRepository.GetUserByUserNameOrNullAsync(requestMessage.UserName.ToUpperInvariant());
            if(user == null)
            {
                return reply.AddObjectNotFoundError();
            }

            if(!_hasher.Equals(user.PasswordHash, requestMessage.Password, user.Salt))
            {
                return reply.AddNotAuthorizedError();
            }

            var role = await _roleRepository.GetRoleByIdOrNullAsync(user.RoleId);
            if(role == null)
            {
                return reply.AddObjectNotFoundError();
            }

            return reply;
        }

        protected override async Task<ReplyMessage<TokenInfo>> ExecuteAsync(GetTokenRequestMessage request)
        {
            var reply = new ReplyMessage<TokenInfo>();

            var identity = await GetIdentityAsync(request.UserName, request.Password);
            if(identity == null)
            {
                reply.AddObjectNotFoundError();
                return reply;
            }

            var now = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                    issuer: AuthenticationOptions.ValidIssuer,
                    audience: AuthenticationOptions.ValidAudience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthenticationOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(AuthenticationOptions.IssuerSigningKey, SecurityAlgorithms.HmacSha256));

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            reply.Value = new TokenInfo { Token = encodedToken };

            return reply;
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUserNameOrNullAsync(username);
            var role = await _roleRepository.GetRoleByIdOrNullAsync(user.RoleId);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            return claimsIdentity;
        }
    }
}
