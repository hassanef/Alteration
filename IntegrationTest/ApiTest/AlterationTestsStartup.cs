using Alteration;
using Alteration.Infrastructure.Context;
using Application.Service;
using DomainModel.Context;
using DomainModel.Repositories;
using Infrastructure.Context;
using Infrastructure.NServiceBus.Configuration;
using Infrastructure.NServiceBus.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IntegrationTest.ApiTest
{
    public class AlterationTestsStartup : Startup
    {
        public AlterationTestsStartup(IConfiguration env) : base(env)
        {
           
        }
        protected override void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IOrderAlterationRepository, OrderAlteraionRepository>();

            services.AddTransient<IOrderAlterationService, OrderAlterationService>();

            services.AddTransient<IContextReadOnly, ApplicationContextReadOnly>();


            var str = Configuration.GetConnectionString("ConnectionString");


            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
               Configuration.GetConnectionString("ConnectionString")));


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
        }
    }
}
