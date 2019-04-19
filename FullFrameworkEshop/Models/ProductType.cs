﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullFrameworkEshop.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string ProductTypeName { get; set; }

        public ICollection<Product> Products { get; set; }

    }

}