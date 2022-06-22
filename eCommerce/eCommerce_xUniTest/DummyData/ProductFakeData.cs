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
                },

                new ProductReadDto()
                {
                    CreatedDate = DateTime.Now,
                    Description = "test2 Description",
                    Price = "1200000",
                    ProductName = "Giay da",
                    UpdatedDate = DateTime.Now,
                },
             };
        }

        public static ProductCreateDto itemProduct()
        {
            return new ProductCreateDto()
            {            
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Description = "test Description",
                Price = "100000",
                ProductName = "Giay test2"
            };
        }
    }
}
