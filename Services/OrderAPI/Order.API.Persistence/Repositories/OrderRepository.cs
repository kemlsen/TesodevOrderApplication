using Order.API.Application.Interfaces.Repository;
using Order.API.Application.Wrappers;
using Order.API.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Domain.Entities.Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
