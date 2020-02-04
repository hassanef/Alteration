using DomainModel.IntegrationEventModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alteration.Application.Services.BackgroundTaskServices.IBackgroundTaskServices
{
    public interface IBackgroundTaskLocalIntegrationEventService
    {

        Task<List<LocalIntegrationEvent>> GetAllReadyToPulishAndUpdateTheirStatuses();

        Task UpdateAllInQueueToProcessToReadyToPublish();

        Task UpdateAsync(LocalIntegrationEvent model);
       

    }
}
