﻿using Microsoft.AspNetCore.Http;

namespace eCommerce_SharedViewModels.EntitiesDto.Categories
{
    public class CategoryCreateDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
