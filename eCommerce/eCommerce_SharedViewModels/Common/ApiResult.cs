﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_SharedViewModels.Common
{
    public class ApiResult<T>   
    {
        public bool IsSuccessed { get; set; }
        public T data { get; set; }
        public string Message { get; set; }
        public string errorMessage { get; set; }
        public T ResultObj { get; set; }
    }
}