using Framework.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel.IntegrationEventModels;

namespace DomainModel.Repositories
{
    public interface ILocalIntegrationEventRepository : IRepository<LocalIntegrationEvent>
    {

        Task<LocalIntegrationEvent> SaveLocalIntegrationEvent<T>(Guid messageId, T ticketDeletedEvent);

        Task UpdateLocalIntegrationEvent(long eventId);
    }
}
