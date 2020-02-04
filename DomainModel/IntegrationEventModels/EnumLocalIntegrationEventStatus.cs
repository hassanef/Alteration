using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.IntegrationEventModels
{
    public enum EnumLocalIntegrationEventStatus
    {
        Pending = 0,
        ReadyToPublish =1 ,
        InQueueToProcess = 2,
        Published = 3,
        Error = 4
    }
}
