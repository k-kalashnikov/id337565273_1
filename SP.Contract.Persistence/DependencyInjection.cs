using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SP.Contract.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
