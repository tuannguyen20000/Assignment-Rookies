﻿namespace eCommerce_xUniTest.DummyData
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
                    ImagessURL = "imagesurl1/url",
                    Price = "200000",
                    ProductName = "Dep ca sau",
                    UpdatedDate = DateTime.Now,
                },

                new ProductReadDto()
                {
                    CreatedDate = DateTime.Now,
                    Description = "test2 Description",
                    ImagessURL = "imagesUrl2/url",
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
                CategoryId = 1,               
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Description = "test Description",
                ImagessURL = "test Images",
                Price = "100000",
                ProductName = "Giay test2"
            };
        }
    }
}