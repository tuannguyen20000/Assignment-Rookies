﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Categories
{
    public class CategoryUpdateDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
