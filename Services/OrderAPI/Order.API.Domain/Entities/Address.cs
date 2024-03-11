using Order.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CityCode { get; set; }
    }
}
