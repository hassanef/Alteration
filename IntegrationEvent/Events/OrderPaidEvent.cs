using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationEvent.Events
{
    public class OrderPaidEvent:IEvent
    {
        public int OrderAlterationId { get; private set; }
        public OrderPaidEvent(int orderALterationId)
        {
            OrderAlterationId = orderALterationId;
        }
    }
}
