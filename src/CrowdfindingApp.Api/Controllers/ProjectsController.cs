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
        private readonly ProjectModerationRequestHandler _projectModerationRequestHandler;
        private readonly ProjectSearchRequestHandler _projectSearchRequestHandler;
        private readonly GetCountriesRequestHandler _getCountriesRequestHandler;
        private readonly GetCitiesRequestHandler _getCitiesRequestHandler;
        private readonly GetCategoriesRequestHandler _getCategoriesRequestHandler;
        private readonly ProjectCardSearchRequestHandler _projectCardSearchRequestHandler;
        private readonly RemoveProjectRequestHandler _removeProjectRequestHandler;

        public ProjectsController(IResourceProvider resourceProvider,
            SaveDraftProjectRequestHandler saveDraftProjectRequestHandler,
            ProjectModerationRequestHandler projectModerationRequestHandler,
            GetCountriesRequestHandler getCountriesRequestHandler,
            GetCitiesRequestHandler getCitiesRequestHandler,
            GetCategoriesRequestHandler getCategoriesRequestHandler,
            RemoveProjectRequestHandler removeProjectRequestHandler,
            ProjectCardSearchRequestHandler projectCardSearchRequestHandler,
            ProjectSearchRequestHandler projectSearchRequestHandler) : base(resourceProvider)
        {
            _saveDraftProjectRequestHandler = saveDraftProjectRequestHandler ?? throw new ArgumentNullException(nameof(saveDraftProjectRequestHandler));
            _projectModerationRequestHandler = projectModerationRequestHandler ?? throw new ArgumentNullException(nameof(projectModerationRequestHandler));
            _projectSearchRequestHandler = projectSearchRequestHandler ?? throw new ArgumentNullException(nameof(projectSearchRequestHandler));
            _getCountriesRequestHandler = getCountriesRequestHandler ?? throw new ArgumentNullException(nameof(getCountriesRequestHandler));
            _getCitiesRequestHandler = getCitiesRequestHandler ?? throw new ArgumentNullException(nameof(getCitiesRequestHandler));
            _getCategoriesRequestHandler = getCategoriesRequestHandler ?? throw new ArgumentNullException(nameof(getCategoriesRequestHandler));
            _projectCardSearchRequestHandler = projectCardSearchRequestHandler ?? throw new ArgumentNullException(nameof(projectCardSearchRequestHandler));
            _removeProjectRequestHandler = removeProjectRequestHandler ?? throw new ArgumentNullException(nameof(removeProjectRequestHandler));
        }

        [HttpDelete("{projectId}")]
        [Authorize]
        public async Task<IActionResult> Remove([FromRoute]string projectId)
        {
            var reply = await _removeProjectRequestHandler.HandleAsync(new RemoveProjectRequestMessage(projectId), User);
            return Respond(reply);
        }

        [HttpPost(Endpoints.Project.SaveDraft)]
        [Authorize]
        public async Task<IActionResult> Save(SaveDraftProjectRequestMessage request)
        {
            var reply = await _saveDraftProjectRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpPost(Endpoints.Project.Moderate)]
        [Authorize]
        public async Task<IActionResult> Moderate(ProjectModerationRequestMessage request)
        {
            var reply = await _projectModerationRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpPost(Endpoints.Project.Search)]
        [Authorize]
        public async Task<IActionResult> Search(ProjectSearchRequestMessage request)
        {
            var reply = await _projectSearchRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpPost(Endpoints.Project.Cards)]
        [Authorize]
        public async Task<IActionResult> Cards(ProjectCardSearchRequestMessage request)
        {
            var reply = await _projectCardSearchRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpGet(Endpoints.Project.Countries)]
        [Authorize]
        public async Task<IActionResult> Countries()
        {
            var reply = await _getCountriesRequestHandler.HandleAsync(new CountriesSearchRequestMessage(), User);
            return Respond(reply);
        }

        [HttpGet(Endpoints.Project.Cities)]
        [Authorize]
        public async Task<IActionResult> Cities()
        {
            var reply = await _getCitiesRequestHandler.HandleAsync(new CitiesSearchRequestMessage(), User);
            return Respond(reply);
        }

        [HttpGet(Endpoints.Project.Categories)]
        [Authorize]
        public async Task<IActionResult> Categories()
        {
            var reply = await _getCategoriesRequestHandler.HandleAsync(new CategoriesSearchRequestMessage(), User);
            return Respond(reply);
        }

    }
}
