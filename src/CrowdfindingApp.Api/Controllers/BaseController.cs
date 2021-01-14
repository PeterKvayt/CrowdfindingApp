using System;
using System.Net;
using CrowdfindingApp.Common.DataTransfers.Errors;
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
                if(error.Message.IsPresent())
                {
                    error.Key = null;
                }
                else if(error.Key.IsPresent())
                {
                    var message = _resourceProvider.GetString(error.Key, error.Parameters ?? Array.Empty<object>());

                    if(!message.Equals(error.Key, StringComparison.InvariantCultureIgnoreCase))
                    {
                        error.Message = message;
                        error.Key = null;
                    }
                    else
                    {
                        error.Message = "Not found message for key";
                    }
                }
                else
                {
                    error.Key = "EmptyKey";
                    error.Message = "Not found message for key";
                }
            }

            return HttpStatusCode.BadRequest;
        }
    }
}
