using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("get-list-cart")]
        public async Task<IActionResult> GetListCart(string UserId)
        {
            var result = await _cartService.GetListAsync(UserId);
            return Ok(result);
        }

        [HttpPost]
        [Route("create-cart")]
        public async Task<IActionResult> Create([FromBody] CartCreateDto request)
        {
            var result = await _cartService.CreateAsync(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        [Route("update-cart/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] CartUpdateDto request)
        {
            var result = await _cartService.UpdateAsync(Id,request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-cart/{Id}/{userId}")]
        public async Task<IActionResult> Delete(int Id, string userId)
        {
            var result = await _cartService.DeleteAsync(Id, userId);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
    }
}
