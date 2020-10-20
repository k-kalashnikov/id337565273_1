using System;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SP.Consumers.Models;
using SP.Contract.API.Consumers.Contracts.Get;
using SP.Contract.API.Services;
using SP.Contract.Events.Request.GetContract;
using SP.Contract.Events.Request.GetOrganizationContracts;
using SP.Market.EventBus.RMQ.Shared.Models.Platform.Contracts.UpdatePurchaseContract;
using SP.Market.EventBus.RMQ.Shared.Models.Platform.Purchases.GetPurchases;

namespace SP.Contract.API.Extensions
{
    public static class MassTransitStartupExtensions
    {
        public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                AddConsumers(x);

                AddRequestClient(x);

                x.AddBus(provider =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(new Uri($"rabbitmq://{config["RMQClient:Host"]}/"), hostConfig =>
                        {
                            hostConfig.Username(config["RMQClient:UserName"]);
                            hostConfig.Password(config["RMQClient:Password"]);
                        });

                        AddReceiveEndpoints(cfg, provider);
                    });

                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var servicesProvider = scope.ServiceProvider;
                    var receiveObserver = servicesProvider.GetRequiredService<IReceiveObserver>();
                    var publishObserver = servicesProvider.GetRequiredService<IPublishObserver>();

                    bus.ConnectReceiveObserver(receiveObserver);
                    bus.ConnectPublishObserver(publishObserver);

                    return bus;
                });
            });

            services.AddSingleton<IHostedService, BusService>();
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());

            return services;
        }

        private static void AddReceiveEndpoint<T>(
            string name, IRabbitMqBusFactoryConfigurator configurator, IServiceProvider provider)
            where T : class, IConsumer
        {
            configurator.ReceiveEndpoint(name, configure =>
            {
                configure.ConfigureConsumer<T>(provider);
            });
        }

        private static void AddRequestClient(IRegistrationConfigurator serviceCollectionConfigurator)
        {
            serviceCollectionConfigurator.AddRequestClient<GetAccountsByIdsRequest>();
            serviceCollectionConfigurator.AddRequestClient<GetOrganizationsRequest>();
            serviceCollectionConfigurator.AddRequestClient<GetPurchasesRequest>();
            serviceCollectionConfigurator.AddRequestClient<UpdatePurchaseContractRequest>();
        }

        private static void AddConsumers(IRegistrationConfigurator serviceCollectionConfigurator)
        {
            serviceCollectionConfigurator.AddConsumer<GetContractConsumer>();
            serviceCollectionConfigurator.AddConsumer<GetOrganizationContractsConsumer>();
        }

        private static void AddReceiveEndpoints(IRabbitMqBusFactoryConfigurator cfg, IServiceProvider provider)
        {
            AddReceiveEndpoint<GetContractConsumer>(nameof(GetContractRequest), cfg, provider);
            AddReceiveEndpoint<GetOrganizationContractsConsumer>(nameof(GetOrganizationContractsRequest), cfg, provider);
    }
    }
}
