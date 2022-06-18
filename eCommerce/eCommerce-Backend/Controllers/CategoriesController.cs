using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
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

        [HttpPost]
        [Route("create-category")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto request)
        {
            var result = await _categoryService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
