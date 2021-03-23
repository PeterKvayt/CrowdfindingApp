using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Messages.Rewards;
using CrowdfindingApp.Core.Services.Rewards.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewardsController : BaseController
    {
        private readonly GetPublicRewardsByProjectIdRequestHandler _getPublicRewardsByProjectIdRequestHandler;

        public RewardsController(IResourceProvider resourceProvider,
            GetPublicRewardsByProjectIdRequestHandler getPublicRewardsByProjectIdRequestHandler
            ) : base(resourceProvider)
        {
            _getPublicRewardsByProjectIdRequestHandler = getPublicRewardsByProjectIdRequestHandler ?? throw new NullReferenceException(nameof(getPublicRewardsByProjectIdRequestHandler));
        }

        [HttpGet(Endpoints.Reward.GetByProjectId + "/{projectId}")]
        public async Task<IActionResult> Search([FromRoute] string projectId)
        {
            var reply = await _getPublicRewardsByProjectIdRequestHandler.HandleAsync(new GetPublicRewardsByProjectIdRequestMessage(projectId), User);
            return Respond(reply);
        }
    }
}
