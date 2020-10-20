using System;
using App.Metrics;
using App.Metrics.Extensions.Configuration;
using App.Metrics.Reporting.InfluxDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SP.Contract.API.Extensions
{
    public static class AppMetricsServiceCollectionExtensions
    {
        public static IServiceCollection AddAppMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            var influxDbOptions = new MetricsReportingInfluxDbOptions();
            configuration.GetSection(nameof(MetricsReportingInfluxDbOptions)).Bind(influxDbOptions);

            services.AddMetrics(
                builder =>
                {
                    builder.Configuration.ReadFrom(configuration);
                    builder.Report.ToInfluxDb(influxDbOptions);
                });

            services.AddMetricsTrackingMiddleware();

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(5);
                options.Predicate = (p) => true;
            });

            services.AddAppMetricsHealthPublishing();
            services.AddMetricsReportingHostedService();
            return services;
        }
    }
}
