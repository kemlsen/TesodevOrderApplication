using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Application.Interfaces.Repository
{
    public interface IOrderRepository : IGenericRepository<Domain.Entities.Order>
    {
    }
}
