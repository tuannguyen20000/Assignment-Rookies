using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.ProductDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("create-product")]
        public async Task<IActionResult> Create([FromForm] ProductsCreateDto request)
        {
            var result = await _productService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles =UserRoles.Admin)]
        [Route("get-paging-product")]
        public async Task<IActionResult> GetPagingProduct([FromQuery] ProductPagingDto request)
        {
            var result = await _productService.GetPagingProduct(request);
            return Ok(result);
        }
    }
}
