using Alteration.Application.Services.BackgroundTaskServices.IBackgroundTaskServices;
using DomainModel.IntegrationEventModels;
using Framework.BackgroundServiceConfigurations;
using Framework.NServiceBusHelper;
using Infrastructure.NServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alteration.AlterationTasks
{
    public class EventPublisher : BackgroundService
    {


        readonly BackgroundServiceConfiguration _taskConfig;
        readonly IBackgroundTaskLocalIntegrationEventService _backgroundTaskLocalIntegrationEventService;
        readonly INServiceBusEndpoint _endpoint;
        public EventPublisher(IConfiguration configuration
            , IBackgroundTaskLocalIntegrationEventService backgroundTaskLocalIntegrationEventService
            , INServiceBusEndpoint nServiceBusEndpoint)
        {
            _taskConfig = configuration.GetSection("Task_EventPublisher").Get<BackgroundServiceConfiguration>();
            _backgroundTaskLocalIntegrationEventService = backgroundTaskLocalIntegrationEventService;
            _endpoint = nServiceBusEndpoint;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(new TimeSpan(0, 0, _taskConfig.Interval), stoppingToken);

                await ProccessEvents();

                if (_taskConfig.IsFireAndForget == true)
                    break;
            }
            await Task.CompletedTask;
        }

        private async Task ProccessEvents()
        {
            List<LocalIntegrationEvent> localIntegrationEvents = await _backgroundTaskLocalIntegrationEventService.GetAllReadyToPulishAndUpdateTheirStatuses();
            foreach (var @event in localIntegrationEvents)
            {
                object obj = @event.JsonBoby.DeserializeJson();
                var messageId = @event.UniqueId;

                messageId = await _endpoint.Publish(obj, messageId).ConfigureAwait(false);

                @event.UniqueId = messageId;
                @event.Status = (int)EnumLocalIntegrationEventStatus.Published;
                await _backgroundTaskLocalIntegrationEventService.UpdateAsync(@event);
            }
        }
    }
}
