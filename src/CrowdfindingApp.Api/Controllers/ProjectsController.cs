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
        private readonly OwnerProjectCardSearchRequestHandler _ownerProjectCardSearchRequestHandler;
        private readonly RemoveProjectRequestHandler _removeProjectRequestHandler;
        private readonly GetByIdRequestHandler _getByIdRequestHandler;
        private readonly SetProjectStatusRequestHandler _setProjectStatusRequestHandler;
        private readonly UnsafeProjectCardSearchRequestHandler _usnafeProjectCardsearchRequestHandler;
        private readonly OpenedProjectCardSearchRequestHandler _openedProjectCardSearchRequestHandler;

        public ProjectsController(IResourceProvider resourceProvider,
            SaveDraftProjectRequestHandler saveDraftProjectRequestHandler,
            ProjectModerationRequestHandler projectModerationRequestHandler,
            GetCountriesRequestHandler getCountriesRequestHandler,
            GetCitiesRequestHandler getCitiesRequestHandler,
            GetCategoriesRequestHandler getCategoriesRequestHandler,
            RemoveProjectRequestHandler removeProjectRequestHandler,
            OwnerProjectCardSearchRequestHandler ownerProjectCardSearchRequestHandler,
            GetByIdRequestHandler getByIdRequestHandler,
            SetProjectStatusRequestHandler setProjectStatusRequestHandler,
            UnsafeProjectCardSearchRequestHandler usnafeProjectCardsearchRequestHandler,
            OpenedProjectCardSearchRequestHandler openedProjectCardSearchRequestHandler,
            ProjectSearchRequestHandler projectSearchRequestHandler) : base(resourceProvider)
        {
            _saveDraftProjectRequestHandler = saveDraftProjectRequestHandler ?? throw new ArgumentNullException(nameof(saveDraftProjectRequestHandler));
            _projectModerationRequestHandler = projectModerationRequestHandler ?? throw new ArgumentNullException(nameof(projectModerationRequestHandler));
            _projectSearchRequestHandler = projectSearchRequestHandler ?? throw new ArgumentNullException(nameof(projectSearchRequestHandler));
            _getCountriesRequestHandler = getCountriesRequestHandler ?? throw new ArgumentNullException(nameof(getCountriesRequestHandler));
            _getCitiesRequestHandler = getCitiesRequestHandler ?? throw new ArgumentNullException(nameof(getCitiesRequestHandler));
            _getCategoriesRequestHandler = getCategoriesRequestHandler ?? throw new ArgumentNullException(nameof(getCategoriesRequestHandler));
            _ownerProjectCardSearchRequestHandler = ownerProjectCardSearchRequestHandler ?? throw new ArgumentNullException(nameof(ownerProjectCardSearchRequestHandler));
            _removeProjectRequestHandler = removeProjectRequestHandler ?? throw new ArgumentNullException(nameof(removeProjectRequestHandler));
            _getByIdRequestHandler = getByIdRequestHandler ?? throw new ArgumentNullException(nameof(getByIdRequestHandler));
            _setProjectStatusRequestHandler = setProjectStatusRequestHandler ?? throw new ArgumentNullException(nameof(setProjectStatusRequestHandler));
            _usnafeProjectCardsearchRequestHandler = usnafeProjectCardsearchRequestHandler ?? throw new ArgumentNullException(nameof(usnafeProjectCardsearchRequestHandler));
            _openedProjectCardSearchRequestHandler = openedProjectCardSearchRequestHandler ?? throw new ArgumentNullException(nameof(openedProjectCardSearchRequestHandler));
        }

        [HttpPost(Endpoints.Project.SetStatus)]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> SetStatus(SetProjectStatusRequestMessage request)
        {
            var reply = await _setProjectStatusRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpPost(Endpoints.Project.Search)]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Search(ProjectSearchRequestMessage request)
        {
            var reply = await _usnafeProjectCardsearchRequestHandler.HandleAsync(request, User);
            return Respond(reply);
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

        [HttpPost(Endpoints.Project.OwnerProjects)]
        [Authorize]
        public async Task<IActionResult> OwnerProjectSearch(ProjectSearchRequestMessage request)
        {
            var reply = await _ownerProjectCardSearchRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpPost(Endpoints.Project.OpenedProjects)]
        [Authorize]
        public async Task<IActionResult> OpenedProjectSearch(ProjectSearchRequestMessage request)
        {
            var reply = await _openedProjectCardSearchRequestHandler.HandleAsync(request, User);
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

        [HttpGet("{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] string projectId)
        {
            var reply = await _getByIdRequestHandler.HandleAsync(new GetProjectByIdRequestMessage(projectId), User);
            return Respond(reply);
        }
    }
}
