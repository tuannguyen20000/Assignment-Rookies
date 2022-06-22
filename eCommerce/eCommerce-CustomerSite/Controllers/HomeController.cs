using eCommerce_CustomerSite.Api.IServices;
using eCommerce_CustomerSite.Models;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eCommerce_CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Page401()
        {
            return View();
        }
    }
}