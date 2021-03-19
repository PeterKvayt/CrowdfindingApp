using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Messages.Files;
using CrowdfindingApp.Core.Services.FileService.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FilesController : BaseController
    {
        private readonly SaveImageRequestHandler _saveImageRequestHandler;

        public FilesController(IResourceProvider resourceProvider,
            SaveImageRequestHandler saveImageRequestHandler
            ) : base(resourceProvider)
        {
            _saveImageRequestHandler = saveImageRequestHandler ?? throw new NullReferenceException(nameof(saveImageRequestHandler));
        }

        [HttpPost(Endpoints.Files.SaveImage)]
        public async Task<IActionResult> Search(SaveImageRequestMessage request)
        {
            var reply = await _saveImageRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }
    }
}
