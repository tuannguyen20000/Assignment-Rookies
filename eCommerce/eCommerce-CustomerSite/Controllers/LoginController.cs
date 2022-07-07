using eCommerce_CustomerSite.Api.IServices;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;
using eCommerce_SharedViewModels.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eCommerce_CustomerSite.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApi _userApi;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public LoginController(IUserApi userApi, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userApi = userApi;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(LoginDto request)
        {
            var result = await _userApi.AuthenticateAsync(request); // Token response
            if (!result.IsSuccessed)
            {
                TempData["error"] = result.Message;
            }
            var userPrincipal = ValidateToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };
            _httpContextAccessor.HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> ModalLogin(LoginDto request)
        {
            var result = await _userApi.AuthenticateAsync(request); // Token response
            if (!result.IsSuccessed)
            {
                return Json(new {success = false, responseText = result.Message});
            }
            else
            {
                var userPrincipal = ValidateToken(result.ResultObj);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = false
                };
                _httpContextAccessor.HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);
                return Json(new {success = true, newUrl = Url.Action("Index", "Home")});
            }
        }


        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["JWT:ValidAudience"];
            validationParameters.ValidIssuer = _configuration["JWT:ValidIssuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove(SystemConstants.AppSettings.Token);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var result = await _userApi.RegisterAsync(request);
            if (!result.IsSuccessed)
            {
                TempData["error"] = result.Message;
                return RedirectToAction("Index", "Login");
            }
            TempData["success"] = result.ResultObj;
            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public async Task<IActionResult> ModalRegister(RegisterDto request)
        {
            var result = await _userApi.RegisterAsync(request); // Token response
            if (!result.IsSuccessed)
            {
                return Json(new { success = false, responseText = result.Message });
            }
            return Json(new { success = true, responseText = result.ResultObj, newUrl = Url.Action("Index", "Login") });
        }
    }
}
