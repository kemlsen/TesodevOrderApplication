using Customer.API.Application.Helpers;
using Customer.API.Application.Interfaces.Repository;
using Customer.API.Persistence.Context;
using Customer.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost; Database=CustomerAPIDb; Port=5432; User Id=postgres; Password=123456; Timeout=30; Command Timeout=30;");
            });

            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<IValidationHelper, ValidationHelper>();
        }
    }
}
