using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductImage;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;
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
            var result = await _productService.GetPagingAsync(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-list-product")]
        public async Task<IActionResult> GetListProduct()
        {
            var result = await _productService.GetListAsync();
            return Ok(result);
        }

        [HttpGet("get-by-id/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _productService.GetByIdAsync(Id);
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
            var result = await _productService.GetImageByIdAsync(imageId);
            if (!result.IsSuccessed)
            {
                return BadRequest("Cannot find image");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id}/get-image-list-by-product-id")]
        public async Task<IActionResult> GetListImageByProductId(int Id)
        {
            var result = await _productService.GetListImageByProductIdAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id}/get-avg-by-id")]
        public async Task<IActionResult> GetAvgRatingById(int Id)
        {
            var result = await _productService.GetAvgRatingByIdAsync(Id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        [Route("create-product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            var result = await _productService.CreateAsync(request);
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

        [HttpPost]
        [Authorize]
        [Route("soft-delete/{Id}")]
        public async Task<IActionResult> SoftDelete(int Id)
        {
            var result = await _productService.SoftDeleteAsync(Id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("{Id}/create-image")]
        public async Task<IActionResult> CreateImage(int Id, [FromForm] ProductImageCreateDto request)
        {
            var result = await _productService.AddImageAsync(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("{Id}/add-comment")]
        public async Task<IActionResult> AddComment(int Id, [FromBody] ProductRatingCreateDto request)
        {
            var result = await _productService.AddCommentAsync(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Consumes("multipart/form-data")]
        [Route("update-product/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] ProductUpdateDto request)
        {
            var result = await _productService.UpdateAsync(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpPut]
        [Authorize]
        [Route("update-image/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateDto request)
        {
            var result = await _productService.UpdateImageAsync(imageId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Route("{Id}/categories-assign")]
        public async Task<IActionResult> CategoryAssign(int Id, [FromBody] CategoryAssignDto request)
        {
            var result = await _productService.CategoryAssignAsync(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpDelete]
        [Authorize]
        [Route("remove-image/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            var result = await _productService.RemoveImageAsync(imageId);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
