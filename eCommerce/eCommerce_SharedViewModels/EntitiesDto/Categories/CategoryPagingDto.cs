﻿using eCommerce_SharedViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.EntitiesDto.Categories
{
    public class CategoryPagingDto : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? CategoriesId { get; set; }
    }
}
