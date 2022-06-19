using eCommerce_CustomerSite.Api.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_CustomerSite.Controllers
{    public class LoginController : Controller
    {
        private readonly IUserApi _userApi;
        public LoginController(IUserApi userApi)
        {
            _userApi = userApi;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(LoginDto request )
        {
            var result = await _userApi.Authenticate(request); // Token response
            if (result.ResultObj == null)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> ModalLogin(LoginDto request)
        {
            var result = await _userApi.Authenticate(request); // Token response
            if (result.ResultObj == null)
            {
                return Json(new 
                { 
                    success = false, 
                    responseText = result.errorMessage,
                });
            }
            else
            {
                return Json(new
                {
                    success = true,
                    newUrl = Url.Action("Index", "Home")
                }); ;
            }
        }
    }
}
