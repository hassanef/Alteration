using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Infra.NServiceBus.Interfaces
{
    public interface INServiceBusEndpoint
    {

        IEndpointInstance EndpointInstance { get; }
    }
}
