using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SP.Contract.API.Extensions;
using SP.Contract.API.Filters;
using SP.Contract.API.Middleware;
using SP.Contract.API.Observers;
using SP.Contract.API.Services;
using SP.Contract.Application;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Settings;
using SP.FileStorage.Client;

namespace SP.Contract.API
{
    public class Startup
    {
        private readonly Assembly _assemblyApplication = typeof(HandlerBase<,>).GetTypeInfo().Assembly;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(Configuration)
                .AddOptions()
                .AddInfrastructure(Configuration, Environment)
                .AddPersistence(Configuration)
                .AddAutoMapper(_assemblyApplication)
                .AddMassTransitService(Configuration)
                .AddAppHealthChecks(Configuration)
                /*.AddAppMetrics(Configuration)*/
                .AddAppSwagger(Configuration)
                .AddApplication();

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddControllers(options =>
                    options.Filters.Add(new CustomExceptionFilterAttribute()))
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // https://github.com/jasontaylordev/NorthwindTraders/issues/76
            // FluentValidation validators execute twice, in the MVC pipeline and the MediatR pipeline #76
            // .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>());
            services.AddValidatorsFromAssemblyContaining<IApplicationDbContext>();

            services.AddTransient<IReceiveObserver, ReceiveObserver>();
            services.AddTransient<IPublishObserver, PublishObserver>();
            services.AddFileStorageClient(opt =>
                {
                    opt.Uri = Configuration["Services:FileStorageService"];
                })
                .AddHostedService<BucketCheck>();
            services.AddOptions()
                .Configure<ContractSettings>(Configuration);

            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", $"swagger by {_assemblyApplication.GetName().Name} V1");
            });

            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true
            })
            .UseHealthChecks("/hcex", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            /*app.UseMetricsAllMiddleware();*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<ContractService>();
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            DefineEnvironment(app, env);

            env.AutoMapperConfigure(_assemblyApplication);
        }

        private void DefineEnvironment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var environmentService = app.ApplicationServices.GetRequiredService<IHostingEnvironmentService>();
            environmentService.SetEnvironment(env.IsProduction());
        }
    }
}
