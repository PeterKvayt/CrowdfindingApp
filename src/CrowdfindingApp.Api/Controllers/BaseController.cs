using System;
using System.Net;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Messages;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        private IResourceProvider _resourceProvider;

        public BaseController(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
        }

        protected IActionResult Respond<TReply>(ReplyMessage<TReply> reply)
        {
            var status = HttpStatusCode.OK;
            
            if(!reply.Success)
            {
                status = PrepareErrors(reply);
            }

            return StatusCode((int)status, reply);
        }

        protected IActionResult Respond(ReplyMessageBase reply)
        {
            var status = HttpStatusCode.OK;

            if(!reply.Success)
            {
                status = PrepareErrors(reply);
            }

            return StatusCode((int)status, reply);
        }

        private HttpStatusCode PrepareErrors(ReplyMessageBase reply)
        {
            foreach(var error in reply.Errors)
            {
                if(error.Message.IsPresent() || error.Key.IsNullOrEmpty())
                {
                    continue;
                }

                error.Message = _resourceProvider.GetString(error.Key, error.Parameters ?? Array.Empty<object>());
                error.Key = null;
            }

            return HttpStatusCode.BadRequest;
        }
    }
}
