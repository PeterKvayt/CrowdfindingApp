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
        private readonly GetUserOrdersRequestHandler _getUserOrdersRequestHandler;
        private readonly GetProjectOrdersRequestHandler _getProjectOrdersRequestHandler;

        public OrdersController(IResourceProvider resourceProvider,
            OrderSearchRequestHandler orderSearchRequestHandler,
            AcceptOrderRequestHandler acceptOrderRequestHandler,
            GetProjectOrdersRequestHandler getProjectOrdersRequestHandler,
            GetUserOrdersRequestHandler getUserOrdersRequestHandler
            ) : base(resourceProvider)
        {
            _orderSearchRequestHandler = orderSearchRequestHandler ?? throw new NullReferenceException(nameof(orderSearchRequestHandler));
            _acceptOrderRequestHandler = acceptOrderRequestHandler ?? throw new NullReferenceException(nameof(acceptOrderRequestHandler));
            _getUserOrdersRequestHandler = getUserOrdersRequestHandler ?? throw new NullReferenceException(nameof(getUserOrdersRequestHandler));
            _getProjectOrdersRequestHandler = getProjectOrdersRequestHandler ?? throw new NullReferenceException(nameof(getProjectOrdersRequestHandler));
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders()
        {
            var reply = await _getUserOrdersRequestHandler.HandleAsync(new GetUserOrdersRequestMessage(), User);
            return Respond(reply);
        }

        [HttpGet(Endpoints.Order.ProjectOrders + "/{projectId}")]
        [Authorize]
        public async Task<IActionResult> ProjectOrders([FromRoute] string projectId)
        {
            var reply = await _getProjectOrdersRequestHandler.HandleAsync(new GetProjectOrdersRequestMessage { Id = projectId }, User);
            return Respond(reply);
        }
    }
}
