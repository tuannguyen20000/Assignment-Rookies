using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;

namespace eCommerce_CustomerSite.Api.IServices
{
    public interface IUserApi
    {
        Task<ApiResult<string>> Authenticate(LoginDto request);
    }
}
