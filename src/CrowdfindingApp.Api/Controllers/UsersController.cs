using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Services.User.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        private GetTokenRequestHandler _getTokenHandler;
        private RegisterRequestHandler _registerHandler;

        public UsersController(GetTokenRequestHandler tokenHandler, RegisterRequestHandler registerHandler, IResourceProvider resourceProvider) : base(resourceProvider)
        {
            _getTokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
            _registerHandler = registerHandler ?? throw new ArgumentNullException(nameof(registerHandler));
        }

        [HttpPost(Endpoints.User.Token)]
        public async Task<IActionResult> GetTokenAsync(GetTokenRequestMessage request)
        {
            var reply = await _getTokenHandler.HandleAsync(request);
            return Respond(reply);
        }

        [HttpPost(Endpoints.User.Register)]
        public async Task<IActionResult> GetTokenAsync(RegisterRequestMessage request)
        {
            var reply = await _registerHandler.HandleAsync(request);
            return Respond(reply);
        }

        [HttpGet(Endpoints.User.ForgotPassword)]
        public async Task<IActionResult> ForgotPasswordAsync(GetTokenRequestMessage request)
        {
            throw new NotImplementedException();
        }

        [HttpGet(Endpoints.User.ResetPassword)]
        [Authorize]
        public async Task<IActionResult> ResetPasswordAsync(GetTokenRequestMessage request)
        {
            throw new NotImplementedException();
        }

        [HttpGet(Endpoints.User.UserInfo)]
        [Authorize]
        public async Task<IActionResult> UserInfoAsync(GetTokenRequestMessage request)
        {
            throw new NotImplementedException();
        }

        [HttpPut(Endpoints.User.UpdateUser)]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfoAsync(GetTokenRequestMessage request)
        {
            throw new NotImplementedException();
        }

        [HttpPut(Endpoints.User.ChangePassword)]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(GetTokenRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
