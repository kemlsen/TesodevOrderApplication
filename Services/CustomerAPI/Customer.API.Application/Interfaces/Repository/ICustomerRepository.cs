using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Application.Interfaces.Repository
{
    public interface ICustomerRepository : IGenericRepository<Domain.Entities.Customer>
    {
    }
}
