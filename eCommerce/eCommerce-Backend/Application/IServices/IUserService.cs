using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;
using eCommerce_SharedViewModels.EntitiesDto.User;

namespace eCommerce_Backend.Application.IServices
{
    public interface IUserService
    {
        Task<ApiResult<ResponseAuth>> Authenticate(LoginDto request);
        Task<ApiResult<string>> Register(RegisterDto request);
        Task<ApiResult<string>> RegisterAdmin(RegisterDto request);
        Task<ApiResult<PagedResult<UserReadDto>>> GetPaging(UserPagingDto request);
        Task<ApiResult<UserReadDto>> GetById(string UserId);
        Task<ApiResult<UserReadDto>> GetByUserName(string UserName);
        Task<List<UserReadDto>> GetList();
    }
}
