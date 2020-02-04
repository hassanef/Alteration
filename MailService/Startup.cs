using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MailService.Application.NServiceBus.Configuration;
using MailService.Application.NServiceBus.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailService
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



            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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


        #region Middlewares 


        /// <summary>
        /// Registeration of services
        /// </summary>
        /// <param name="services"></param>
        private void AddServices(IServiceCollection services)
        {
            services.AddSingleton<INServiceBusEndpoint, NServiceBusEndpoint>();


            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<INServiceBusEndpoint>();
        }

        /// <summary>
        /// More about HealthCheck is in https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/monitor-app-health
        /// </summary>
        /// <param name="services"></param>
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
        //        checks.AddSqlCheck("Receiver", CurrentEndpointConnectionString, TimeSpan.FromMinutes(minutes));
        //    });
        //}
        #endregion
    }
}
