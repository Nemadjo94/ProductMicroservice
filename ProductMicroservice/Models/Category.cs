﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroservice.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
