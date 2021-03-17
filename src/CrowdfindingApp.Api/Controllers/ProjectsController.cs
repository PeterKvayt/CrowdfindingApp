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
        private readonly GetCountriesRequestHandler _getCountriesRequestHandler;
        private readonly GetCitiesRequestHandler _getCitiesRequestHandler;

        public ProjectsController(IResourceProvider resourceProvider,
            SaveDraftProjectRequestHandler saveDraftProjectRequestHandler,
            GetCountriesRequestHandler getCountriesRequestHandler,
            GetCitiesRequestHandler getCitiesRequestHandler,
            ProjectSearchRequestHandler projectSearchRequestHandler) : base(resourceProvider)
        {
            _saveDraftProjectRequestHandler = saveDraftProjectRequestHandler ?? throw new ArgumentNullException(nameof(saveDraftProjectRequestHandler));
            _projectSearchRequestHandler = projectSearchRequestHandler ?? throw new ArgumentNullException(nameof(projectSearchRequestHandler));
            _getCountriesRequestHandler = getCountriesRequestHandler ?? throw new ArgumentNullException(nameof(getCountriesRequestHandler));
            _getCitiesRequestHandler = getCitiesRequestHandler ?? throw new ArgumentNullException(nameof(getCitiesRequestHandler));
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
    }
}
