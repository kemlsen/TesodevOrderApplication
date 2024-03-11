using Customer.API.Application.Interfaces.Repository;
using Customer.API.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Domain.Entities.Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<List<Domain.Entities.Customer>> GetAll()
        {
            return await dbContext.Customers
                .Include(c => c.Address)
                .Include(c => c.Orders)
                .ToListAsync();
        }

        public override async Task<Domain.Entities.Customer> GetById(Guid id)
        {
            return await dbContext.Customers
                .Include(c => c.Address)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
