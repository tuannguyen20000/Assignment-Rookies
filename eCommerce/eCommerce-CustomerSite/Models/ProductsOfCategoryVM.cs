using eCommerce_SharedViewModels.Common;
using eCommerce_SharedViewModels.EntitiesDto.Categories;
using eCommerce_SharedViewModels.EntitiesDto.Product;

namespace eCommerce_CustomerSite.Models
{
    public class ProductsOfCategoryVM
    {
        public CategoryReadDto Category { get; set; }

        public PagedResult<ProductReadDto> Products { get; set; }
    }
}
