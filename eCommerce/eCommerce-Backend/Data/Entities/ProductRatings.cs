﻿namespace eCommerce_Backend.Data.Entities
{
    public class ProductRatings
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int Rating { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
