using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.User;
using CrowdfindingApp.Common.Handlers;
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

        public GetTokenRequestHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        protected override async Task<ReplyMessage<TokenInfo>> ExecuteAsync(GetTokenRequestMessage request)
        {
            var reply = new ReplyMessage<TokenInfo>();

            var identity = await GetIdentityOrNullAsync(request.Login, request.Password);
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

        private async Task<ClaimsIdentity> GetIdentityOrNullAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUserNameOrNullAsync(username);
            if(user == null)
            {
                return null;
            }

            var role = await _roleRepository.GetRoleByIdOrNullAsync(user.RoleId);
            if(role == null)
            {
                return null;
            }

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
