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

        public UsersController(GetTokenRequestHandler tokenHandler)
        {
            _getTokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
        }


        [HttpGet("token")]
        public async Task<IActionResult> GetTokenAsync(GetTokenRequestMessage request)
        {
            var reply = await _getTokenHandler.HandleAsync(request);

            return Json(reply);
        }
    }
}
