using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;

namespace eCommerce_Backend.Application.IServices
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginDto request);
        Task<ApiResult<string>> Register(RegisterDto request);
        Task<ApiResult<string>> RegisterAdmin(RegisterDto request);
    }
}
