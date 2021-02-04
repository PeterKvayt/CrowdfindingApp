using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Core.Services.Projects.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : BaseController
    {
        private readonly SaveDraftProjectRequestHandler _saveDraftProjectRequestHandler;
        private readonly ProjectSearchRequestHandler _projectSearchRequestHandler;

        public ProjectsController(IResourceProvider resourceProvider,
            SaveDraftProjectRequestHandler saveDraftProjectRequestHandler,
            ProjectSearchRequestHandler projectSearchRequestHandler) : base(resourceProvider)
        {
            _saveDraftProjectRequestHandler = saveDraftProjectRequestHandler ?? throw new ArgumentNullException(nameof(saveDraftProjectRequestHandler));
            _projectSearchRequestHandler = projectSearchRequestHandler ?? throw new ArgumentNullException(nameof(projectSearchRequestHandler));
        }

        [HttpPost(Endpoints.Project.SaveDraft)]
        [Authorize]
        public async Task<IActionResult> GetTokenAsync(SaveDraftProjectRequestMessage request)
        {
            var reply = await _saveDraftProjectRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpPost(Endpoints.Project.Search)]
        [Authorize]
        public async Task<IActionResult> Search(ProjectSearchRequestMessage request)
        {
            var reply = await _projectSearchRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }
    }
}
