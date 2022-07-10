using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Data.Entities;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;
using eCommerce_SharedViewModels.EntitiesDto.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static eCommerce_SharedViewModels.Utilities.Constants.SystemConstants;

namespace eCommerce_Backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<Users> userManager,
            RoleManager<Roles> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<ApiResult<ResponseAuth>> AuthenticateAsync(LoginDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var roles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, string.Join(";", roles))
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new ApiSuccessResult<ResponseAuth>(new ResponseAuth()
                {
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    user = request.Username,
                    isInRole = string.Join("", roles)
                });
            }
            return new ApiErrorResult<ResponseAuth>(ErrorMessage.LoginFail);
        }

        public async Task<ApiResult<UserReadDto>> GetByIdAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return new ApiErrorResult<UserReadDto>(ErrorMessage.UserNameExists);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserReadDto()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserId = user.Id,
                UserName = user.UserName,
                IsInRole = string.Join(";", roles)
            };
            return new ApiSuccessResult<UserReadDto>(userVm);
        }

        public async Task<ApiResult<UserReadDto>> GetByUserNameAsync(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return new ApiErrorResult<UserReadDto>(ErrorMessage.UserNameExists);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userVm = new UserReadDto()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserId = user.Id,
                UserName = user.UserName,
                IsInRole = string.Join(";", roles)
            };
            return new ApiSuccessResult<UserReadDto>(userVm);
        }

        public async Task<List<UserReadDto>> GetListAsync()
        {
            using (_userManager)
            {
                var data = await _userManager.Users.Select(x => new UserReadDto
                {
                    UserId = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                }).ToListAsync();
                return data;
            }
        }

        public async Task<ApiResult<PagedResult<UserReadDto>>> GetPagingAsync(UserPagingDto request)
        {
            var query = _userManager.Users;         
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                || x.PhoneNumber.Contains(request.Keyword));
            }
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserReadDto()
                {
                    UserId = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,

                }).ToListAsync();
            var pagedResult = new PagedResult<UserReadDto>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserReadDto>>(pagedResult);
        }

        public async Task<ApiResult<string>> RegisterAsync(RegisterDto request)
        {
            if (request.Password != request.ConfirmPassword)
                return new ApiErrorResult<string>(ErrorMessage.WrongPasswordConfirm);
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
                return new ApiErrorResult<string>(ErrorMessage.UserNameExists);

            Users user = new()
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

        public async Task<ApiResult<string>> RegisterAdminAsync(RegisterDto request)
        {
            if (request.Password != request.ConfirmPassword)
                return new ApiErrorResult<string>(ErrorMessage.WrongPasswordConfirm);
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
                return new ApiErrorResult<string>(ErrorMessage.UserNameExists);

            Users user = new()
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
                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
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
