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
        private ForgotPasswordRequestHandler _forgotPasswordRequestHandler;
        private ResetPasswordRequestHandler _resetPasswordRequestHandler;
        private GetUserInfoRequestHandler _getUserInfoRequestHandler;
        private UpdateUserRequestHandler _updateUserRequestHandler;

        public UsersController(GetTokenRequestHandler tokenHandler,
            RegisterRequestHandler registerHandler,
            IResourceProvider resourceProvider,
            ForgotPasswordRequestHandler forgotPasswordRequestHandler,
            ResetPasswordRequestHandler resetPasswordRequestHandler,
            GetUserInfoRequestHandler getUserInfoRequestHandler,
            UpdateUserRequestHandler updateUserRequestHandler) : base(resourceProvider)
        {
            _getTokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
            _registerHandler = registerHandler ?? throw new ArgumentNullException(nameof(registerHandler));
            _forgotPasswordRequestHandler = forgotPasswordRequestHandler ?? throw new ArgumentNullException(nameof(forgotPasswordRequestHandler));
            _resetPasswordRequestHandler = resetPasswordRequestHandler ?? throw new ArgumentNullException(nameof(resetPasswordRequestHandler));
            _getUserInfoRequestHandler = getUserInfoRequestHandler ?? throw new ArgumentNullException(nameof(getUserInfoRequestHandler));
            _updateUserRequestHandler = updateUserRequestHandler ?? throw new ArgumentNullException(nameof(updateUserRequestHandler));
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

        [HttpGet(Endpoints.User.ForgotPassword + "/{email}")]
        public async Task<IActionResult> ForgotPasswordAsync(string email)
        {
            var request = new ForgotPasswordRequestMessage { Email = email };
            var reply = await _forgotPasswordRequestHandler.HandleAsync(request);
            return Respond(reply);
        }

        [HttpPost(Endpoints.User.ResetPassword)]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestMessage request)
        {
            var reply = await _resetPasswordRequestHandler.HandleAsync(request);
            return Respond(reply);
        }

        [HttpGet(Endpoints.User.UserInfo + "/{id}")]
        [Authorize]
        public async Task<IActionResult> UserInfoAsync(string id)
        {
            var request = new GetUserInfoRequestMessage { Id = id };
            var reply = await _getUserInfoRequestHandler.HandleAsync(request);
            return Respond(reply);
        }

        [HttpPut(Endpoints.User.UpdateUser)]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfoAsync(UpdateUserRequestMessage request)
        {
            var reply = await _updateUserRequestHandler.HandleAsync(request);
            return Respond(reply);
        }

        [HttpPut(Endpoints.User.ChangePassword)]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(GetTokenRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
