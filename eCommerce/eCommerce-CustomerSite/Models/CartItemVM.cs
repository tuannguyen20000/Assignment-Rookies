﻿namespace eCommerce_CustomerSite.Models
{
    public class CartItemVM
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
    }
}
