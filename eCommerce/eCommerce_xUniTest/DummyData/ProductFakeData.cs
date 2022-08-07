using eCommerce_Backend.Data.Entities;
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
                    Price = 200000,
                    ProductName = "Dep ca sau",
                    UpdatedDate = DateTime.Now,       
                    CategoryId = 1,
                },

                new ProductReadDto()
                {
                    CreatedDate = DateTime.Now,
                    Description = "test2 Description",
                    Price = 1200000,
                    ProductName = "Giay da",
                    UpdatedDate = DateTime.Now,
                    CategoryId = 1,
                },
             };
        }

        public static List<Products> ListProductst()
        {
            return new List<Products>()
            {
                new Products()
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    Description = "test1 Description",
                    Price = 200000,
                    ProductName = "Dep ca sau",
                    UpdatedDate = DateTime.Now,        
                    ProductQuantity = 100,
                    Status = Status.Available,                   
                },

                new Products()
                {
                    Id= 2,
                    CreatedDate = DateTime.Now,
                    Description = "test2 Description",
                    Price = 1200000,
                    ProductName = "Giay da",
                    UpdatedDate = DateTime.Now,
                    ProductQuantity = 200,
                    Status = Status.Available,
                },
             };
        }

        public static ProductCreateDto createItemProduct()
        {
            return new ProductCreateDto()
            {            
                Description = "test Description",
                Price = 100000,
                ProductName = "Giay test2",         
            };
        }

        public static ProductUpdateDto UpdatetemProduct()
        {
            return new ProductUpdateDto()
            {
                Description = "update Description",
                Price = 100000,
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
                CategoriesId = null,
                Keyword = null
            };
        }
    }
}
