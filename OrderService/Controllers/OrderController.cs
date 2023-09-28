using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderModel order)
        {
            await _publishEndpoint.Publish<OrderModel>(order);

            return Ok();
        }
    }
}