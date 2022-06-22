using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductImage;
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
        [Authorize]
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
            var result = await _productService.GetById(Id);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Consumes("multipart/form-data")]
        [Route("update-product/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] ProductUpdateDto request)
        {
            var result = await _productService.Update(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("soft-delete/{Id}")]
        public async Task<IActionResult> SoftDelete(int Id)
        {
            var result = await _productService.SoftDelete(Id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("{Id}/create-image")]
        public async Task<IActionResult> CreateImage(int Id, [FromForm] ProductImageCreateDto request)
        {
            var imageId = await _productService.AddImage(Id, request);
            if (!imageId.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(imageId);
        }

        [HttpPut]
        [Authorize]
        [Route("update-image/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateDto request)
        {
            var result = await _productService.UpdateImage(imageId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("get-image-by-id/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _productService.GetImageById(imageId);
            if (image == null)
            {
                return BadRequest("Cannot find image");
            }
            return Ok(image);
        }

        [HttpGet]
        [Route("{Id}/get-image-list-by-product-id")]
        public async Task<IActionResult> GetListImageByProductId(int Id)
        {
            var image = await _productService.GetListImageByProductId(Id);
            return Ok(image);
        }

        [HttpDelete]
        [Authorize]
        [Route("remove-image/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            var result = await _productService.RemoveImage(imageId);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("{Id}/categories-assign")]
        public async Task<IActionResult> CategoryAssign(int Id, [FromBody] CategoryAssignDto request)
        {
            var result = await _productService.CategoryAssign(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
