



namespace eCommerce_xUniTest.DummyData
{
    public class CatgoryFakeData
    {
        public static List<CategoryReadDto> GetCategory()
        {
            return new List<CategoryReadDto>()
            {
                new CategoryReadDto()
                {
                    CategoryName = "test category name 1",
                    Description = "test description 1",
                    Id = 10000,
                    Status = Status.Available,
                    ThumbnailImage = "image category 1"
                },

                new CategoryReadDto()
                {
                    CategoryName = "test category name 2",
                    Description = "test description 2",
                    Id = 10001,
                    Status = Status.Disable,
                    ThumbnailImage = "image category 2"
                },

                new CategoryReadDto()
                {
                    CategoryName = "test category name 3",
                    Description = "test description 3",
                    Id = 10002,
                    Status = Status.Available,
                    ThumbnailImage = "image category 3"
                },
             };
        }

        public static CategoryCreateDto createItemCategory()
        {
            return new CategoryCreateDto()
            {
                CategoryName = "category create 1",
                Description = "category create description 1",
            };
        }

        public static CategoryUpdateDto UpdatetemCategory()
        {
            return new CategoryUpdateDto()
            {
                CategoryName = "category update 1",
                Description = "category update description 1",
            };
        }


        public static PagedResult<CategoryReadDto> PageResultCategoryRead()
        {
            return new PagedResult<CategoryReadDto>()
            {
                Items = GetCategory(),
                PageIndex = 1,
                PageSize = 12,
                TotalRecords = 12
            };
        }

        public static CategoryPagingDto PagingItemCategory()
        {
            return new CategoryPagingDto()
            {
                PageIndex = 1,
                PageSize = 12,
                CategoriesId = null,
                Keyword = null
            };
        }
    }
}
