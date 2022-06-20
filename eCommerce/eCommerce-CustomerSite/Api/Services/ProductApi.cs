using eCommerce_CustomerSite.Api.Common;
using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Product;
using Newtonsoft.Json;
using System.Text;

namespace eCommerce_CustomerSite.ApiComsumes.Services
{
    public class ProductApi : HttpService,IProductApi
    {
        public ProductApi(HttpClient httpClient) : base(httpClient){}

        public async Task<ApiResult<bool>> Create(ProductCreateDto request)
        {
            var data = await PostAsync<ApiResult<bool>>($"/api/Products/", request);
            if (!data.IsSuccessed)
            {
                return new ApiErrorResult<bool>(data.Message);
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<PagedResult<ProductReadDto>> GetPagingProduct(ProductPagingDto request)
        {
            var data = await GetAsync<PagedResult<ProductReadDto>>($"/api/Products/GetPagingProduct?&" +
                $"pageIndex={request.PageIndex}&pageSize={request.PageSize}");
            return data;
        }
    }
}
