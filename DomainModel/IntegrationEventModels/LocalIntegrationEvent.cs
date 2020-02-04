using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.IntegrationEventModels
{
    public class LocalIntegrationEvent
    {
        public long Id { get; set; }
        public Guid? UniqueId { get; set; }
        public string ModelName { get; set; }
        public string ModelNamespace { get; set; }
        public string JsonBoby { get; set; }
        public byte[] BinaryBody { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        

    }
}
