using eCommerce_SharedViewModels.Enums;

namespace eCommerce_Backend.Data.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public List<CategoryImages> CategoryImages { get; set; }
    }
}
