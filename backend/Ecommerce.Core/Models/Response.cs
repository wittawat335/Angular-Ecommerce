﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Response<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
    }
}
