using Microsoft.AspNetCore.Mvc;
using WidgetStore.Services;
using WidgetStore.Models;

namespace WidgetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateOrder(CreateOrderCommand command)
        {
            var orderId = await _orderService.CreateOrderAsync(command);
            return Ok(new { OrderId = orderId });
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderReadModel>> GetOrder(string orderId, [FromQuery] string userEmail)
        {
            var order = await _orderService.GetOrderAsync(orderId, userEmail);
            return Ok(order);
        }

        [HttpGet("user/{userEmail}")]
        public async Task<ActionResult<List<OrderReadModel>>> GetUserOrders(string userEmail)
        {
            var orders = await _orderService.GetUserOrdersAsync(userEmail);
            return Ok(orders);
        }

        [HttpPost("{orderId}/ship")]
        public async Task<IActionResult> ShipOrder(string orderId, [FromQuery] string userEmail)
        {
            await _orderService.UpdateShippingDateAsync(orderId, userEmail, DateTime.UtcNow);
            return Ok();
        }
    }
}