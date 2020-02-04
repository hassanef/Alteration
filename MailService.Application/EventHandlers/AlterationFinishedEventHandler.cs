using IntegrationEvent.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Application.EventHandlers
{
   
    public class AlterationFinishedEventHandler : IHandleMessages<AlterationFinishedEvent>
    {
        public AlterationFinishedEventHandler()
        {
        }


        public async Task Handle(AlterationFinishedEvent message, IMessageHandlerContext context)
        {

            //handle message
            var orderAlterationId = message.OrderAlterationId;

        }
    }
}
