using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.API.Application.Helpers;
using Order.API.Application.Interfaces.Repository;
using Order.API.Persistence.Context;
using Order.API.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<OrderDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost; Database=OrderAPIDb; Port=5432; User Id=postgres; Password=123456; Timeout=30; Command Timeout=30;");
            });

            serviceCollection.AddTransient<IOrderRepository, OrderRepository>();
            serviceCollection.AddTransient<HttpClient>();
            serviceCollection.AddTransient<IValidationHelper,ValidationHelper>();
        }
    }
}
