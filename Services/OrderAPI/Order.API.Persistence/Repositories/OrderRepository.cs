using Microsoft.EntityFrameworkCore;
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

        public override async Task<List<Domain.Entities.Order>> GetAll()
        {
            return await dbContext.Orders
               .Include(c => c.Product)
               .Include(c => c.Address)
               .AsNoTracking()
               .ToListAsync();
        }

        public override async Task<List<Domain.Entities.Order>> GetAllById(Guid id)
        {
            return await dbContext.Orders
               .Include(c => c.Product)
               .Include(c => c.Address)
               .AsNoTracking()
               .Where(x => x.Id == id).ToListAsync();
        }

        public override async Task<Domain.Entities.Order> GetById(Guid id)
        {
            return await dbContext.Orders
               .Include(c => c.Product)
               .Include(c => c.Address)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
