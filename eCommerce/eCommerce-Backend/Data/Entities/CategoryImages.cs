namespace eCommerce_Backend.Data.Entities
{
    public class CategoryImages
    {
        public int Id { get; set; }
        public int CategoriesId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }
        public long FileSize { get; set; }

        public Categories Categories { get; set; }
    }
}
