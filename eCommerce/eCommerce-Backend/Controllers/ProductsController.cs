using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("get-paging-product")]
        public async Task<IActionResult> GetPagingProduct([FromQuery] ProductPagingDto request)
        {
            var result = await _productService.GetPaging(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-list-product")]
        public async Task<IActionResult> GetListProduct()
        {
            var result = await _productService.GetList();
            return Ok(result);
        }


        [HttpPost]
        [Route("create-product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            var result = await _productService.Create(request);
            if (result == null)
            {
                return BadRequest(result.errorMessage);
            }
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("get-by-id/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var user = await _productService.GetById(Id);
            return Ok(user);
        }

        [HttpPut("update-product/{Id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int Id, [FromForm] ProductUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Update(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("soft-delete/{Id}")]

        public async Task<IActionResult> SoftDelete(int Id)
        {
            var result = await _productService.SoftDelete(Id);
            return Ok(result);
        }
    }
}
