using eCommerce_CustomerSite.Api.Common;
using eCommerce_CustomerSite.Api.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using eCommerce_SharedViewModels.EntitiesDto.Register;

namespace eCommerce_CustomerSite.Api.Services
{
    public class UserApi : HttpService, IUserApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApi(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<string>> Authenticate(LoginDto request)
        {
            var data = await GetToken<ApiResult<ResponseAuth>>($"https://localhost:7211/api/Authenticate/login/", request);
            if (!data.IsSuccessed)
            {
                return new ApiErrorResult<string>(data.Message);
            }
            return new ApiSuccessResult<string>(data.ResultObj.accessToken);
        }

        public async Task<ApiResult<string>> Register(RegisterDto request)
        {
/*            var sessions = _httpContextAccessor.HttpContext.Session.GetString("JWT");*/
            var data = await PostAsync<ApiResult<string>>($"https://localhost:7211/api/Authenticate/register/", request);
            if (!data.IsSuccessed)
            {
                return new ApiErrorResult<string>(data.Message);
            }
            return new ApiSuccessResult<string>(data.ResultObj);
        }
    }
}
