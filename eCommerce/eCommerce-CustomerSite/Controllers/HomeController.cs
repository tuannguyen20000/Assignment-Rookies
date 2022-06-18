using eCommerce_CustomerSite.Api.IServices;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eCommerce_CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserApi _userApi;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserApi userApi)
        {
            _userApi = userApi;
            _logger = logger;
        }

        public async Task<IActionResult> Index(LoginDto request)
        {
            request.Username = "tuanAdmin";
            request.Password = "tuanAdmin@123";
            var result = await _userApi.Authenticate(request);
            if (result.ResultObj == null)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}