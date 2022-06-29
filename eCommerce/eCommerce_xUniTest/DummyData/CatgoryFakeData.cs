



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

                },

                new CategoryReadDto()
                {
                },
             };
        }

        public static CategoryCreateDto createItemCategory()
        {
            return new CategoryCreateDto()
            {

            };
        }

        public static CategoryUpdateDto UpdatetemCategory()
        {
            return new CategoryUpdateDto()
            {

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

        public static CategoryPagingDto PagingItemProduct()
        {
            return new CategoryPagingDto()
            {

            };
        }
    }
}
