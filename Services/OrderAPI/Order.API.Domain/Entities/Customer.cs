﻿using Order.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public List<Order> Orders { get; set; }
    }
}
