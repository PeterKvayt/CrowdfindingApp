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

        public OrdersController(IResourceProvider resourceProvider,
            OrderSearchRequestHandler orderSearchRequestHandler
            ) : base(resourceProvider)
        {
            _orderSearchRequestHandler = orderSearchRequestHandler ?? throw new NullReferenceException(nameof(orderSearchRequestHandler));
        }

        [HttpPost(Endpoints.Order.Search)]
        [Authorize]
        public async Task<IActionResult> Search(OrderSearchRequestMessage request)
        {
            var reply = await _orderSearchRequestHandler.HandleAsync(request, User);
            return Respond(reply);
        }
    }
}
