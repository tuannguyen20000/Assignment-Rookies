using eCommerce_Backend.Application.Interfaces;
using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IOrderService orderService, IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Authorize]
        [Route("get-paging-order")]
        public async Task<IActionResult> GetPagingOrder([FromQuery] OrderPagingDto request)
        {
            var result = await _orderService.GetPagingAsync(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-by-id/{Id}")]
        public IActionResult GetById(int Id)
        {
            var result = _unitOfWork.Order.GetById(Id);
            return Ok(result);
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

        [HttpPut]
        [Authorize]
        [Route("update-status-order/{Id}")]
        public async Task<IActionResult> UpdateStatus(int Id, [FromForm] OrderUpdateDto request)
        {
            var result = await _orderService.UpdateStatusAsync(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
