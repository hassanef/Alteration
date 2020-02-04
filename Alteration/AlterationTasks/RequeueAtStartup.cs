using Alteration.Application.Services.BackgroundTaskServices.IBackgroundTaskServices;
using Framework.BackgroundServiceConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alteration.AlterationTasks
{
    public class RequeueAtStartup : BackgroundService
    {

        readonly BackgroundServiceConfiguration _taskConfig;
        readonly IBackgroundTaskLocalIntegrationEventService _backgroundTaskLocalIntegrationEventService;
        public RequeueAtStartup(IConfiguration configuration
            , IBackgroundTaskLocalIntegrationEventService backgroundTaskLocalIntegrationEventService

            )
        {
            _taskConfig = configuration.GetSection("Task_RequeueAtStartup").Get<BackgroundServiceConfiguration>();
            _backgroundTaskLocalIntegrationEventService = backgroundTaskLocalIntegrationEventService;
        }


        /// <summary>
        /// If AlterationMicroservice is stopped by any exceptions or incidents, some IntegrationEvents that are in InQueue status, won't be published on next run of API
        /// this BackgroundService starts once, every time that BackgroundTasks Api starts, and it updates all InQueue statuses to ReadyToPublish 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(new TimeSpan(0, 0, _taskConfig.Interval), stoppingToken);


                await _backgroundTaskLocalIntegrationEventService.UpdateAllInQueueToProcessToReadyToPublish();


                if (_taskConfig.IsFireAndForget == true)
                    break;
            }
            await Task.CompletedTask;
        }
    }
}
