using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Messages.User;
using CrowdfindingApp.Core.Services.User.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    public class UsersController : Controller
    {
        private GetTokenRequestHandler _getTokenHandler;
        private RegisterRequestHandler _registerHandler;

        public UsersController(GetTokenRequestHandler tokenHandler, RegisterRequestHandler registerHandler)
        {
            _getTokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
            _registerHandler = registerHandler ?? throw new ArgumentNullException(nameof(registerHandler));
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(GetTokenRequestMessage request)
        {
            var reply = await _getTokenHandler.HandleAsync(request);
            return Json(reply);
        }

        [HttpPost("register")]
        public async Task<IActionResult> GetTokenAsync(RegisterRequestMessage request)
        {
            var reply = await _registerHandler.HandleAsync(request);
            return Json(reply);
        }
    }
}
