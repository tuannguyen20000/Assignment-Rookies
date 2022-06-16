using eCommerce_SharedViewModels.Common;

namespace eCommerce_SharedViewModels.EntitiesDto.ProductDto
{
    public class ProductPagingDto : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}
