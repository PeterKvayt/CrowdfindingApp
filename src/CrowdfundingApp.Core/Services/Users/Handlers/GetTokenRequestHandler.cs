﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.DataTransfers.Users;
using CrowdfundingApp.Common.Extensions;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Immutable;
using CrowdfundingApp.Common.Core.Maintainers.Hasher;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Users;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace CrowdfundingApp.Core.Services.Users.Handlers
{
    public class GetTokenRequestHandler : RequestHandlerBase<GetTokenRequestMessage, ReplyMessage<TokenInfo>, ClaimsIdentity>
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

        protected override async Task<(ReplyMessageBase, ClaimsIdentity)> ValidateRequestMessageAsync(GetTokenRequestMessage requestMessage)
        {
            var (reply, ctx) = await base.ValidateRequestMessageAsync(requestMessage);

            if(requestMessage.Email.IsNullOrEmpty())
            {
                reply.AddValidationError(UserErrorKeys.EmptyEmail);
                return (reply, ctx);
            }

            if(requestMessage.Password.IsNullOrEmpty())
            {
                reply.AddValidationError(UserErrorKeys.EmptyPassword);
                return (reply, ctx);
            }

            var user = await _userRepository.GetUserByEmailAsync(requestMessage.Email.ToUpperInvariant());
            if(user == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, ctx);
            }

            if(!_hasher.Equals(user.PasswordHash, requestMessage.Password, user.Salt))
            {
                reply.AddNotAuthorizedError();
                return (reply, ctx);
            }

            var role = await _roleRepository.GetByIdAsync(user.RoleId);
            if(role == null)
            {
                reply.AddObjectNotFoundError();
                return (reply, ctx);
            }

            ctx = GetIdentity(user, role);

            return (reply, ctx);
        }

        protected override async Task<ReplyMessage<TokenInfo>> ExecuteAsync(GetTokenRequestMessage request, ClaimsIdentity identity)
        {
            return await Task.Run(() =>  Execute(request, identity));
        }

        private ReplyMessage<TokenInfo> Execute(GetTokenRequestMessage request, ClaimsIdentity identity)
        {
            var reply = new ReplyMessage<TokenInfo>();
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

        private  ClaimsIdentity GetIdentity(User user, Role role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsKeys.UserId, user.Id.ToString()),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType)
                new Claim(ClaimsKeys.Roles, role.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            return claimsIdentity;
        }
    }
}
