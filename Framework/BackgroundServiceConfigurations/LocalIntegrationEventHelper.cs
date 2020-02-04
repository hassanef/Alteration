using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.BackgroundServiceConfigurations
{
    public class LocalIntegrationEventHelper<T>
    {
        public long Id { get; set; }
        public Guid MessageId { get; set; }
        public T @Event { get; set; }
    }
}
