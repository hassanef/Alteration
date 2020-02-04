using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationEvent.Events
{
    public class AlterationFinishedEvent:IEvent
    {
        public int OrderAlterationId { get; private set; }
        public AlterationFinishedEvent(int orderALterationId)
        {
            OrderAlterationId = orderALterationId;
        }
    }
}
