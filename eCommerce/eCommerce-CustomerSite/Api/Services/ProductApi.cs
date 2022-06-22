using eCommerce_CustomerSite.Api.Common;
using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using eCommerce_SharedViewModels.EntitiesDto.Product.ProductRating;
using eCommerce_SharedViewModels.Utilities.Constants;
using Newtonsoft.Json;
using System.Text;

namespace eCommerce_CustomerSite.ApiComsumes.Services
{
    public class ProductApi : HttpService,IProductApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApi(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient){
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<bool>> AddComment(int Id, ProductRatingCreateDto request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var data = await PostAsync<ApiResult<bool>>($"https://localhost:7211/api/Products/{Id}/add-comment", request, session);
            if (!data.IsSuccessed)
            {
                return new ApiErrorResult<bool>(data.Message);
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Create(ProductCreateDto request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var data = await PostAsync<ApiResult<bool>>($"https://localhost:7211/api/Products/create-product", request, session);
            if (!data.IsSuccessed)
            {
                return new ApiErrorResult<bool>(data.Message);
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<ProductReadDto>> GetById(int Id)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var data = await GetAsync<ApiResult<ProductReadDto>>($"https://localhost:7211/api/Products/get-by-id/{Id}", session);
            if (!data.IsSuccessed)
            {
                return new ApiErrorResult<ProductReadDto>(data.Message);
            }
            return new ApiSuccessResult<ProductReadDto>(data.ResultObj);
        }

        public async Task<PagedResult<ProductReadDto>> GetPagingProduct(ProductPagingDto request)
        {      
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var data = await GetAsync<PagedResult<ProductReadDto>>($"https://localhost:7211/api/Products/get-paging-product?&" +
                $"pageIndex={request.PageIndex}&pageSize={request.PageSize}", session);
            return data;
        }
    }
}
