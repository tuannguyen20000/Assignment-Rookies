using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto request)
        {
            var result = await _orderService.CreateAsync(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
    }
}
