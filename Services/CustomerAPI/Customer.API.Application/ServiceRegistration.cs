using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Customer.API.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assem = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assem);
            services.AddMediatR(assem);
        }
    }
}
