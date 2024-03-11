using Microsoft.EntityFrameworkCore;
using Order.API.Application.Interfaces.Repository;
using Order.API.Domain.Common;
using Order.API.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OrderDbContext dbContext;
        public GenericRepository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(Guid id)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public Task<List<T>> GetAllById(Guid id)
        {
            return dbContext.Set<T>().Where(x => x.Id == id).ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
