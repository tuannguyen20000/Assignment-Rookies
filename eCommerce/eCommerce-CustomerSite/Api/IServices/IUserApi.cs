using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;

namespace eCommerce_CustomerSite.Api.IServices
{
    public interface IUserApi
    {
        Task<ApiResult<string>> AuthenticateAsync(LoginDto request);
        Task<ApiResult<string>> RegisterAsync(RegisterDto request);
    }
}
