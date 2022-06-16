using eCommerce_CustomerSite.ApiComsumes.Common;
using eCommerce_CustomerSite.ApiComsumes.IServices;
using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.ProductDto;
using eCommerce_SharedViewModels.Utilities.Constants;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace eCommerce_CustomerSite.ApiComsumes.Services
{
    public class ProductApi : BaseApi,IProductApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApi(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<bool>> Create(ProductsCreateDto request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var data = await PostAsync<ApiResult<bool>>($"/api/Products/", httpContent);
            return data;
        }

        public async Task<PagedResult<ProductReadDto>> GetPagingProduct(ProductPagingDto request)
        {
            var data = await GetAsync<PagedResult<ProductReadDto>>($"/api/Products/GetPagingProduct?&" +
                $"pageIndex={request.PageIndex}&pageSize={request.PageSize}");
            return data;
        }
    }
}
