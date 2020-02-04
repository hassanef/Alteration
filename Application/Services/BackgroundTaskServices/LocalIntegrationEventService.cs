using DomainModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Services.BackgroundTaskServices.IBackgroundTaskServices;

namespace Ticket.Application.Services.BackgroundTaskServices
{

    public class LocalIntegrationEventService : ILocalIntegrationEventService 
    {
        readonly ILocalIntegrationEventRepository localIntegrationEventRepository;
        public LocalIntegrationEventService(ILocalIntegrationEventRepository _localIntegrationEventRepository)
        {
            localIntegrationEventRepository = _localIntegrationEventRepository;
        }



    }
}
