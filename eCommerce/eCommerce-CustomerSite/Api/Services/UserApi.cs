using eCommerce_CustomerSite.Api.Common;
using eCommerce_CustomerSite.Api.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;

namespace eCommerce_CustomerSite.Api.Services
{
    public class UserApi : HttpService, IUserApi
    {
        public UserApi(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<ApiResult<string>> Authenticate(LoginDto request)
        {
            var data = await GetToken<ApiResult<string>>($"https://localhost:7211/api/Authenticate/login/", request);
            if (!data.IsSuccessed)
            {
                return new ApiErrorResult<string>(data.Message);
            }
            return new ApiSuccessResult<string>(data.ResultObj);
        }
    }
}
