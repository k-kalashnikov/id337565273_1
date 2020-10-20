using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SP.Contract.Client.Interfaces;
using SP.Contract.Client.Services;
using SP.Contract.Client.Settings;

namespace SP.Contract.Client
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddContractClient(this IServiceCollection services, Action<ContractClientOptions> options)
        {
            services.Configure(options);
            AddContractClient(services);

            return services;
        }

        private static IServiceCollection AddContractClient(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var uri = serviceProvider.GetRequiredService<IOptions<ContractClientOptions>>().Value?.Uri;
            var maxSendMessageSize = serviceProvider.GetRequiredService<IOptions<ContractClientOptions>>().Value?.MaxSendMessageSize;
            var maxReceiveMessageSize = serviceProvider.GetRequiredService<IOptions<ContractClientOptions>>().Value?.MaxReceiveMessageSize;

            services.AddSingleton<IContractClientOptionsService>(new ContractClientOptionsService(new ContractClientOptions
            {
                Uri = uri,
                MaxReceiveMessageSize = maxReceiveMessageSize,
                MaxSendMessageSize = maxSendMessageSize
            }));

            services.AddScoped<IContractClientService, ContractClientService>();

            return services;
        }
    }
}
