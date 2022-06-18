using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;

namespace eCommerce_Backend.Application.IServices
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginDto request);
        Task<ApiResult<bool>> Register(RegisterDto request);
        Task<ApiResult<bool>> RegisterAdmin(RegisterDto request);
    }
}
