using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Application.NServiceBus.Interfaces
{
    public interface INServiceBusEndpoint
    {
        Task<Guid> Publish(object message, Guid? messageId);
        Task<string> Publish(object message);
        Task<string> Send(object message);
    }
}
