﻿using Customer.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
