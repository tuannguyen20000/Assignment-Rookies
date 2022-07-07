using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;
using eCommerce_SharedViewModels.EntitiesDto.User;

namespace eCommerce_Backend.Application.IServices
{
    public interface IUserService
    {
        Task<ApiResult<ResponseAuth>> AuthenticateAsync(LoginDto request);
        Task<ApiResult<string>> RegisterAsync(RegisterDto request);
        Task<ApiResult<string>> RegisterAdminAsync(RegisterDto request);
        Task<ApiResult<PagedResult<UserReadDto>>> GetPagingAsync(UserPagingDto request);
        Task<ApiResult<UserReadDto>> GetByIdAsync(string UserId);
        Task<ApiResult<UserReadDto>> GetByUserNameAsync(string UserName);
        Task<List<UserReadDto>> GetListAsync();
    }
}
