using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.NServiceBusHelper
{
    public abstract class IntegrationCommand
    {
        public Guid Id { get; private set; }
        public DateTime Sent { get; private set; }

        protected IntegrationCommand()
        {
            Id = Guid.NewGuid();
            Sent = DateTime.UtcNow;
        }
    }

}
