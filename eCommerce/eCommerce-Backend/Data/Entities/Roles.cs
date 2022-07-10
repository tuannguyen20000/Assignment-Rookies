using Microsoft.AspNetCore.Identity;

namespace eCommerce_Backend.Data.Entities
{
    public class Roles : IdentityRole<string>
    {
        public string Description { get; set; }
    }
}
