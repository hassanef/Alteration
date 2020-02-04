using Alteration.Application.Services.BackgroundTaskServices.IBackgroundTaskServices;
using DomainModel.IntegrationEventModels;
using DomainModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Application.Services.BackgroundTaskServices
{

    public class BackgroundTaskLocalIntegrationEventService : IBackgroundTaskLocalIntegrationEventService
    {
        readonly IBackgroundTaskLocalIntegrationEventRepository _backgroundTaskLocalIntegrationEventRepository;
        public BackgroundTaskLocalIntegrationEventService(IBackgroundTaskLocalIntegrationEventRepository backgroundTaskLocalIntegrationEventRepository)
        {
            _backgroundTaskLocalIntegrationEventRepository = backgroundTaskLocalIntegrationEventRepository;
        }


        public async Task<List<LocalIntegrationEvent>> GetAllReadyToPulishAndUpdateTheirStatuses()
        {

            

            var query = await _backgroundTaskLocalIntegrationEventRepository.FetchMulti(q => q.Status == (int)EnumLocalIntegrationEventStatus.ReadyToPublish)
                .OrderBy(q => q.Id)
                .Take(50)
                .ToListAsync();

            var selectedIds = query.Select(q => q.Id);

            if (!selectedIds.Any())
                return new List<LocalIntegrationEvent>();

            await _backgroundTaskLocalIntegrationEventRepository.BulkUpdateAsync(q => selectedIds.Contains(q.Id),
                q => new LocalIntegrationEvent()
                {
                    Status = (int)EnumLocalIntegrationEventStatus.InQueueToProcess
                });

            return query;
        }


        /// <summary>
        /// this method fires once when the BackgroundTask starts
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAllInQueueToProcessToReadyToPublish()
        {
            await _backgroundTaskLocalIntegrationEventRepository.BulkUpdateAsync(q => q.Status == (int)EnumLocalIntegrationEventStatus.InQueueToProcess,
                q => new LocalIntegrationEvent()
                {
                    Status = (int)EnumLocalIntegrationEventStatus.ReadyToPublish
                });
        }


        public async Task UpdateAsync(LocalIntegrationEvent model)
        {
            await _backgroundTaskLocalIntegrationEventRepository.UpdateAsync(model);
        }

    }
}
