using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Core.Localization;
using CrowdfindingApp.Common.Core.Messages.Files;
using CrowdfindingApp.Core.Services.FileService.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
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

        [Authorize]
        [DisableRequestSizeLimit]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [HttpPut(Endpoints.Files.SaveImage)]
        public async Task<IActionResult> Search([FromForm(Name = "file")] IFormFile file)
        {
            var reply = await _saveImageRequestHandler.HandleAsync(new SaveImageRequestMessage(file), User);
            return Respond(reply);
        }
    }
}
