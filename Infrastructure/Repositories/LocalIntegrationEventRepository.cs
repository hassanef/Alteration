using Framework.BackgroundServiceConfigurations;
using Framework.IRepositories;
using Framework.NServiceBusHelper;
using Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel.IntegrationEventModels;
using DomainModel.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class LocalIntegrationEventRepository : Repository<LocalIntegrationEvent> , ILocalIntegrationEventRepository
    {
        public LocalIntegrationEventRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }


        public async Task<LocalIntegrationEvent> SaveLocalIntegrationEvent<T>(Guid messageId, T ticketDeletedEvent)
        {
            var jsonSerialization = ticketDeletedEvent.SerializeJson();
            var binarySerialization = ticketDeletedEvent.SerializeBinary();

            var localIntegrationEvent = new DomainModel.IntegrationEventModels.LocalIntegrationEvent()
            {
                JsonBoby = jsonSerialization,
                UniqueId = messageId,
                CreatedAt = DateTime.Now,
                Status = (int)EnumLocalIntegrationEvent.Pending,
                BinaryBody = binarySerialization,
                ModelName = ticketDeletedEvent.GetType().Name,
                ModelNamespace = ticketDeletedEvent.GetType().Namespace
            };
            await CreateAsyncUoW(localIntegrationEvent).ConfigureAwait(false);
            return localIntegrationEvent;
        }


        public async Task UpdateLocalIntegrationEvent(long eventId)
        {
            var localIntegrationEvent = FirstOrDefaultWithReload(q => q.Id == eventId);
            localIntegrationEvent.Status = (int)EnumLocalIntegrationEvent.Ready;
            await UpdateAsync(localIntegrationEvent).ConfigureAwait(false);
        }


    }
}
