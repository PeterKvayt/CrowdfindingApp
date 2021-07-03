using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Core.Localization;
using CrowdfindingApp.Common.Core.Messages.Rewards;
using CrowdfindingApp.Core.Services.Rewards.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RewardsController : BaseController
    {
        private readonly GetPublicRewardsByProjectIdRequestHandler _getPublicRewardsByProjectIdRequestHandler;
        private readonly GetPublicRewardByIdRequestHandler _getPublicRewardByIdRequestHandler;

        public RewardsController(IResourceProvider resourceProvider,
            GetPublicRewardsByProjectIdRequestHandler getPublicRewardsByProjectIdRequestHandler,
            GetPublicRewardByIdRequestHandler getPublicRewardByIdRequestHandler
            ) : base(resourceProvider)
        {
            _getPublicRewardsByProjectIdRequestHandler = getPublicRewardsByProjectIdRequestHandler ?? throw new NullReferenceException(nameof(getPublicRewardsByProjectIdRequestHandler));
            _getPublicRewardByIdRequestHandler = getPublicRewardByIdRequestHandler ?? throw new NullReferenceException(nameof(getPublicRewardByIdRequestHandler));
        }

        [HttpGet(Endpoints.Reward.GetByProjectId + "/{projectId}")]
        public async Task<IActionResult> Search([FromRoute] string projectId)
        {
            var reply = await _getPublicRewardsByProjectIdRequestHandler.HandleAsync(new GetPublicRewardsByProjectIdRequestMessage(projectId), User);
            return Respond(reply);
        }

        [HttpGet("{rewardId}")]
        public async Task<IActionResult> GetPublicById([FromRoute] string rewardId)
        {
            var reply = await _getPublicRewardByIdRequestHandler.HandleAsync(new GetPublicRewardByIdRequestMessage(rewardId), User);
            return Respond(reply);
        }
    }
}
