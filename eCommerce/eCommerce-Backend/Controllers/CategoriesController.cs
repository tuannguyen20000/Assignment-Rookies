using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
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
            var result = await _categoryService.GetPaging(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-list-category")]
        public async Task<IActionResult> GetListCategory()
        {
            var result = await _categoryService.GetList();
            return Ok(result);
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

        [HttpGet("get-by-id/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var user = await _categoryService.GetById(Id);
            return Ok(user);
        }

        [HttpPut("update-category/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromForm] CategoryUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.Update(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("soft-delete/{Id}")]

        public async Task<IActionResult> SoftDelete(int Id)
        {
            var result = await _categoryService.SoftDelete(Id);
            return Ok(result);
        }
    }
}
