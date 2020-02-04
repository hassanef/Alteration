using Framework.Repositories;
using DomainModel.IntegrationEventModels;
using DomainModel.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BackgroundTaskLocalIntegrationEventRepository : Repository<LocalIntegrationEvent> , IBackgroundTaskLocalIntegrationEventRepository
    {
        public BackgroundTaskLocalIntegrationEventRepository(BackgroundTaskDbContext  backgroundTaskDbContext) : base(backgroundTaskDbContext)
        {

        }
    }
}
