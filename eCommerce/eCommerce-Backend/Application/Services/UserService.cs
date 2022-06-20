using eCommerce_Backend.Application.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;

namespace eCommerce_Backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<ApiResult<string>> Authenticate(LoginDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return new ApiErrorResult<string>(ErrorMessage.LoginFail);
        }

        public async Task<ApiResult<string>> Register(RegisterDto request)
        {
            if (request.Password != request.ConfirmPassword)
                return new ApiErrorResult<string>(ErrorMessage.WrongPasswordConfirm);
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
                return new ApiErrorResult<string>(ErrorMessage.UserNameExists);

            IdentityUser user = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new ApiErrorResult<string>(ErrorMessage.UserCreateFail);
            else
            {
                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
                return new ApiSuccessResult<string>(SuccessMessage.UserCreated);
            }
        }

        public async Task<ApiResult<string>> RegisterAdmin(RegisterDto request)
        {
            if (request.Password != request.ConfirmPassword)
                return new ApiErrorResult<string>(ErrorMessage.WrongPasswordConfirm);
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
                return new ApiErrorResult<string>(ErrorMessage.UserNameExists);

            IdentityUser user = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new ApiErrorResult<string>(ErrorMessage.UserCreateFail);
            else
            {
                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                }
                return new ApiSuccessResult<string>(SuccessMessage.UserCreated);
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
