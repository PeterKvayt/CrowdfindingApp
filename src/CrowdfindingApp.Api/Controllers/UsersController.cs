using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Services.User.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        private GetTokenRequestHandler _getTokenHandler;
        private RegisterRequestHandler _registerHandler;
        private ForgotPasswordRequestHandler _forgotPasswordRequestHandler;
        private ResetPasswordRequestHandler _resetPasswordRequestHandler;

        public UsersController(GetTokenRequestHandler tokenHandler,
            RegisterRequestHandler registerHandler,
            IResourceProvider resourceProvider,
            ForgotPasswordRequestHandler forgotPasswordRequestHandler,
            ResetPasswordRequestHandler resetPasswordRequestHandler) : base(resourceProvider)
        {
            _getTokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
            _registerHandler = registerHandler ?? throw new ArgumentNullException(nameof(registerHandler));
            _forgotPasswordRequestHandler = forgotPasswordRequestHandler ?? throw new ArgumentNullException(nameof(forgotPasswordRequestHandler));
            _resetPasswordRequestHandler = resetPasswordRequestHandler ?? throw new ArgumentNullException(nameof(resetPasswordRequestHandler));
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
        [Authorize]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestMessage request)
        {
            var reply = await _resetPasswordRequestHandler.HandleAsync(request);
            return Respond(reply);
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
