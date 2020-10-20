using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using RabbitMQ.Client;

namespace SP.Contract.API.Extensions
{
    public static class HealthChecksServiceCollectionExtensions
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureHttpsForHealthChecksClient("IdentityService");

            services.AddHealthChecks()
                .AddNpgSql($"{configuration["ConnectionStrings:PostgresConnection:connectionString"]}" +
                           $"Database={configuration["ConnectionStrings:PostgresConnection:Database"]}")
                .AddRabbitMQ(
                    $"amqp://{configuration["RMQClient:UserName"]}:{configuration["RMQClient:Password"]}@" +
                    $"{configuration["RMQClient:Host"]}:5672",
                    sslOption: new SslOption(serverName: configuration["RMQClient:Host"], enabled: false),
                    name: "RabbitMQ")
                .AddElasticsearch(configuration["Serilog:Elasticsearch"], name: "ElasticSearch")
                .AddUrlGroup(
                    new Uri(
                        new Uri(configuration["Services:IdentityService"]).GetLeftPart(System.UriPartial.Authority) +
                        "/hc"), name: "IdentityService");
                /*.AddUrlGroup(new Uri(new Uri(configuration["MetricsReportingInfluxDbOptions:InfluxDb:BaseUri"]).GetLeftPart(System.UriPartial.Authority) + "/ping"), name: "InfluxDb");*/

            return services;
        }

        private static void ConfigureHttpsForHealthChecksClient(this IServiceCollection services, string httpClientName)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .RetryAsync(5);

            services.AddHttpClient(httpClientName)
                .AddPolicyHandler(retryPolicy)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                });
        }
    }
}
