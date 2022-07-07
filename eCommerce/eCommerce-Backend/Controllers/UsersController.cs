using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.EntitiesDto.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("get-paging-user")]
        public async Task<IActionResult> GetPaging([FromQuery] UserPagingDto request)
        {
            var result = await _userService.GetPagingAsync(request);
            return Ok(result);
        }


        [HttpGet]
        [Route("get-by-id/{UserId}")]
        public async Task<IActionResult> GetById(string UserId)
        {
            var result = await _userService.GetByIdAsync(UserId);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-by-user-name/{UserName}")]
        public async Task<IActionResult> GetByUserName(string UserName)
        {
            var result = await _userService.GetByUserNameAsync(UserName);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-list-user")]
        public async Task<IActionResult> GetListUser()
        {
            var result = await _userService.GetListAsync();
            return Ok(result);
        }
    }
}
