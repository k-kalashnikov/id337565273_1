using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Persistence;
using SP.Contract.Persistence.Extensions;

namespace SP.Contract.API.Extensions
{
    public static class PersistenceStartupExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.BuildConnectionString()));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddPersistence();
            return services;
        }
    }
}
