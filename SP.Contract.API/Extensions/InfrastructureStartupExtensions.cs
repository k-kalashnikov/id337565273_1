using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.Contract.API.Services;
using SP.Contract.Application;
using SP.Contract.Common;
using SP.Contract.Infrastructure.Services;
using SP.Market.Identity.Client;
using SP.Market.Identity.Common.Interfaces;
using SP.Market.Identity.Common.Services;

namespace SP.Contract.API.Extensions
{
    public static class InfrastructureStartupExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddTransient<IHostingEnvironmentService, HostingEnvironmentService>();
            services.AddTransient(typeof(IRequestClientService<,>), typeof(RequestClientService<,>));
            services.AddMarketIdentity(new AuthenticationClientOptions
            {
                Uri = new UriBuilder($"{configuration.GetSection("Services:IdentityService").Value}"),
                IsIncludeOneFactorAuthotication = false
            });

            var corsParams = configuration.GetSection("Cors").Get<List<string>>();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(corsParams.Where(x => x != null).ToArray())
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            return services;
        }
    }
}
