using eCommerce_SharedViewModels.Common;

namespace eCommerce_SharedViewModels.EntitiesDto.Product
{
    public class ProductPagingDto : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? CategoriesId { get; set; }
    }
}
