using System;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using CrowdfindingApp.Common.Messages.Orders;
using CrowdfindingApp.Core.Services.Orders.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfindingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : BaseController
    {
        private readonly OrderSearchRequestHandler _orderSearchRequestHandler;
        private readonly AcceptOrderRequestHandler _acceptOrderRequestHandler;

        public OrdersController(IResourceProvider resourceProvider,
            OrderSearchRequestHandler orderSearchRequestHandler,
            AcceptOrderRequestHandler acceptOrderRequestHandler
            ) : base(resourceProvider)
        {
            _orderSearchRequestHandler = orderSearchRequestHandler ?? throw new NullReferenceException(nameof(orderSearchRequestHandler));
            _acceptOrderRequestHandler = acceptOrderRequestHandler ?? throw new NullReferenceException(nameof(acceptOrderRequestHandler));
        }

        [HttpPost(Endpoints.Order.Search)]
        [Authorize]
        public async Task<IActionResult> Search(OrderSearchRequestMessage request)
        {
            var reply = await _orderSearchRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Search(AcceptOrderRequestMessage request)
        {
            var reply = await _acceptOrderRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }
    }
}
