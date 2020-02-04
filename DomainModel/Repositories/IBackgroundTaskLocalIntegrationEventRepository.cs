using Framework.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.IntegrationEventModels;

namespace DomainModel.Repositories
{
    public interface IBackgroundTaskLocalIntegrationEventRepository : IRepository<LocalIntegrationEvent>
    {
    }
}
