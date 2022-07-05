using eCommerce_SharedViewModels.Common;

namespace eCommerce_xUniTest.DummyData
{
    public class ProductFakeData
    {
        public static List<ProductReadDto> GetProduct()
        {
            return new List<ProductReadDto>()
            {
                new ProductReadDto()
                {
                    CreatedDate = DateTime.Now,
                    Description = "test1 Description",
                    Price = "200000",
                    ProductName = "Dep ca sau",
                    UpdatedDate = DateTime.Now,       
                    CategoryId = 1,
                },

                new ProductReadDto()
                {
                    CreatedDate = DateTime.Now,
                    Description = "test2 Description",
                    Price = "1200000",
                    ProductName = "Giay da",
                    UpdatedDate = DateTime.Now,
                    CategoryId = 1,
                },
             };
        }

        public static ProductCreateDto createItemProduct()
        {
            return new ProductCreateDto()
            {            
                Description = "test Description",
                Price = "100000",
                ProductName = "Giay test2",         
            };
        }

        public static ProductUpdateDto UpdatetemProduct()
        {
            return new ProductUpdateDto()
            {
                Description = "update Description",
                Price = "100000",
                ProductName = "Giay update",
            };
        }


        public static PagedResult<ProductReadDto> PageResultProductRead()
        {
            return new PagedResult<ProductReadDto>()
            {
               Items = GetProduct(),
               PageIndex = 1,
               PageSize = 12,
               TotalRecords = 12
            };
        }

        public static ProductPagingDto PagingItemProduct()
        {
            return new ProductPagingDto()
            {
                PageIndex = 1,
                PageSize = 12,
                CategoriesId = 1,
                Keyword = null
            };
        }
    }
}
