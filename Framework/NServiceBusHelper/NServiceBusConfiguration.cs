using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.NServiceBusHelper
{

    /// <summary>
    /// this the setting for endpoints
    /// this class is being used to read configuration from appsetting.json file in Endpoints and map to an object of this class
        /// </summary>
    public class NServiceBusConfiguration
    {
        /// <summary>
        /// endpoint or microservice name
        /// </summary>
        public string CuurentEndpoint { get; set; }
        /// <summary>
        /// failed messages in endpoint go to this queue or table
        /// </summary>
        public string SendFailedMessagesTo { get; set; }
        /// <summary>
        /// All messages in endpoint go to this queue or table
        /// </summary>
        public string AuditProcessedMessagesTo { get; set; }
        /// <summary>
        /// local db connection string for the endpoint
        /// </summary>
        public string CurrentEndpointConnectionString { get; set; }
        /// <summary>
        /// Transport db connection string for the All endpoints
        /// </summary>
        public string TransportConnectionString { get; set; }
        public string DefaultSchema { get; set; }
        public string PublisherEndpoint { get; set; }
        public string PublisherSenderTableName { get; set; }
        public string PublisherSchema { get; set; }

    }
}
