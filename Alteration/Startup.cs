using System;
using Alteration.Infrastructure.Context;
using Application.Service;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DomainModel.Context;
using DomainModel.Repositories;
using Infrastructure.Context;
using Infrastructure.Generic.Application.Infrastructure.AutofacModules;
using Infrastructure.NServiceBus.Configuration;
using Infrastructure.NServiceBus.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

namespace Alteration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //AddHealthChecksService(services);

            AddServices(services);



            var container = new ContainerBuilder();


            container.Populate(services);


            container.RegisterModule(new MediatorModule());
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.Migrate();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }




        protected virtual void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<INServiceBusEndpoint, NServiceBusEndpoint>();

            services.AddTransient<IOrderAlterationRepository, OrderAlteraionRepository>();

            services.AddTransient<IOrderAlterationService, OrderAlterationService>();

            services.AddTransient<IContextReadOnly, ApplicationContextReadOnly>();


            var str = Configuration.GetConnectionString("ConnectionString");


            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
               Configuration.GetConnectionString("ConnectionString") ));


            //Implement resilient Entity Framework Core SQL connections
            //https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/implement-resilient-entity-framework-core-sql-connections
            services.AddDbContextPool<BackgroundTaskDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("ConnectionString"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                }));


            services.AddTransient<ILocalIntegrationEventRepository, LocalIntegrationEventRepository>();

            services.AddTransient<IBackgroundTaskLocalIntegrationEventRepository, BackgroundTaskLocalIntegrationEventRepository>();



            // BackgroundServices
            //services.AddHostedService<RequeueAtStartup>();
            //AddCustomHostedService(services , typeof(EventPublisher));
            //AddCustomHostedService(services, typeof(RequeueAtStartup));

            // run NServiceBus IEndpointInstance 
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<INServiceBusEndpoint>();
        }

        //private void AddHealthChecksService(IServiceCollection services)
        //{
        //    var CurrentEndpointConnectionString = Configuration.GetSection("NServiceBusConfiguration:CurrentEndpointConnectionString").Value;
        //    services.AddHealthChecks(checks =>
        //    {
        //        var minutes = 1;
        //        if (int.TryParse(Configuration["HealthCheck:Timeout"], out var minutesParsed))
        //        {
        //            minutes = minutesParsed;
        //        }
        //        checks.AddSqlCheck("Sender", CurrentEndpointConnectionString, TimeSpan.FromMinutes(minutes));
        //    });
        //}

        private void AddCustomHostedService(IServiceCollection services, Type type)
        {
            // background Tasks should start with AddHostedService

            services.AddTransient(type);
            var serviceProvider = services.BuildServiceProvider();

            var intsance = (Microsoft.Extensions.Hosting.BackgroundService)serviceProvider.GetService(type);
            intsance.StartAsync(new System.Threading.CancellationToken()).Wait();
        }
    }
}
