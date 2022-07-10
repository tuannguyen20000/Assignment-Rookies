using Microsoft.AspNetCore.Identity;

namespace eCommerce_Backend.Data.Entities
{
    public class Users : IdentityUser<string>
    {
        public List<Carts> Carts { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
