using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("get-paging-category")]
        public async Task<IActionResult> GetPagingCategory([FromQuery] CategoryPagingDto request)
        {
            var result = await _categoryService.GetPagingAsync(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-list-category")]
        public async Task<IActionResult> GetListCategory()
        {
            var result = await _categoryService.GetListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("get-by-id/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _categoryService.GetByIdAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-list-product-by-Id/{categoryId}")]
        public async Task<IActionResult> GetListProductById(int categoryId)
        {
            var result = await _categoryService.GetListProductByIdAsync(categoryId);
            return Ok(result);
        }

        [HttpPost]
        [Route("create-category")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto request)
        {
            var result = await _categoryService.CreateAsync(request);
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
            var result = await _categoryService.SoftDeleteAsync(Id);
            return Ok(result);
        }


        [HttpPut]
        [Authorize]
        [Route("update-category/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] CategoryUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.UpdateAsync(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



    }
}
