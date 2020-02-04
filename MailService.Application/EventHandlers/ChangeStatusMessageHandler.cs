using IntegrationEvent.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Application.EventHandlers
{
    public class ChangeStatusMessageHandler : IHandleMessages<OrderPaidEvent>
    {
        public ChangeStatusMessageHandler()
        {
        }


        public async Task Handle(OrderPaidEvent message, IMessageHandlerContext context)
        {

            //handle message
            var id = message.OrderAlterationId;

        }
    }
}
