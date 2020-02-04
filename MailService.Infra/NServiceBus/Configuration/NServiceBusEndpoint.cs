using Framework.NServiceBusHelper;
using IntegrationEvent.Events;
using MailService.Infra.NServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using NServiceBus.Transport.SQLServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Infra.NServiceBus.Configuration
{

    /// <summary>
    /// more help can be found here https://docs.particular.net/samples/endpoint-configuration/
    /// </summary>
    public class NServiceBusEndpoint : IDisposable, INServiceBusEndpoint
    {
        private static IEndpointInstance endpointInstance;
        public IEndpointInstance EndpointInstance
        {
            get
            {
                return endpointInstance;
            }
        }


        readonly NServiceBusConfiguration config;
        public NServiceBusEndpoint(IConfiguration _configuration, IServiceCollection services)
        {
            config = _configuration.GetSection("NServiceBusConfiguration").Get<NServiceBusConfiguration>();
            Initialize(services).Wait();
        }



        private async Task Initialize(IServiceCollection services)
        {
            if (endpointInstance != null)
                return;

            if (config == null)
                throw new Exception("NServiceBusConfiguration section in appsetting.json file is not provided");


            var senderDb = config.CurrentEndpointConnectionString; // "Data Source=.;Initial Catalog=Sender;Persist Security Info=True;User ID=sa;Password=AAaa123;Max Pool Size=80";
            var transportDb = config.TransportConnectionString; // "Data Source=.;Initial Catalog=SqlTransport;Persist Security Info=True;User ID=sa;Password=AAaa123;Max Pool Size=80";

            #region SqlServer Transport
            Console.Title = "Ticket.Sender";
            var endpointConfiguration = new EndpointConfiguration(config.CuurentEndpoint);
            endpointConfiguration.SendFailedMessagesTo(config.SendFailedMessagesTo);


            ////Configuring endpoints for monitoring _ SendHeartbeat
            //endpointConfiguration.SendHeartbeatTo(
            //    serviceControlQueue: "Particular.ServiceControl",
            //    frequency: TimeSpan.FromSeconds(15),
            //    timeToLive: TimeSpan.FromSeconds(30));


            //endpointConfiguration.ReportCustomChecksTo(
            //    serviceControlQueue: "Particular.ServiceControl",
            //    timeToLive: TimeSpan.FromSeconds(10));


            // Turn on auditing.
            endpointConfiguration.AuditProcessedMessagesTo(config.AuditProcessedMessagesTo);

            //endpointConfiguration.DisableFeature<MessageDrivenSubscriptions>();

            #region SenderConfiguration

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(transportDb);
            transport.DefaultSchema(config.DefaultSchema);
            transport.UseSchemaForQueue(config.SendFailedMessagesTo, "dbo");
            transport.UseSchemaForQueue(config.AuditProcessedMessagesTo, "dbo");
            transport.UseNativeDelayedDelivery().DisableTimeoutManagerCompatibility();



            //endpointConfiguration.UsePersistence<InMemoryPersistence>();
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
            dialect.Schema(config.DefaultSchema);
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(senderDb);
                });
            persistence.TablePrefix("");
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));

            // Use JSON.NET serializer
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            //Enable the Outbox
            endpointConfiguration.EnableOutbox();

            // Make sure NServiceBus creates queues in RabbitMQ, tables in SQL Server, etc.
            // You might want to turn this off in production, so that DevOps can use scripts to create these.
            endpointConfiguration.EnableInstallers();



            // Define conventions
            //var conventions = endpointConfiguration.Conventions();
            //conventions.DefiningEventsAs(c => c.Namespace != null && c.Name.EndsWith("IntegrationEvent"));

            #endregion
            SqlHelper.CreateSchema(senderDb, config.DefaultSchema);
            SqlHelper.CreateSchema(transportDb, config.DefaultSchema);
            endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            #endregion







            //#region RabbitMQ Transport Configuration
            //var endpointConfiguration = new EndpointConfiguration(config.CuurentEndpoint);

            //// Configure RabbitMQ transport
            //var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            //transport.UseConventionalRoutingTopology();
            //transport.ConnectionString(GetRabbitConnectionString());

            //// Configure persistence
            ////var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            ////persistence.SqlDialect<SqlDialect.MsSqlServer>();
            ////persistence.ConnectionBuilder(connectionBuilder:
            ////    () => new SqlConnection(config.ConnectionString));

            //// Use JSON.NET serializer
            //endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            //// Enable the Outbox
            //endpointConfiguration.EnableOutbox();

            //// Make sure NServiceBus creates queues in RabbitMQ, tables in SQL Server, etc.
            //// You might want to turn this off in production, so that DevOps can use scripts to create these.
            //endpointConfiguration.EnableInstallers();

            //// Turn on auditing.
            //endpointConfiguration.AuditProcessedMessagesTo("audit");

            //// Define conventions
            ////var conventions = endpointConfiguration.Conventions();
            ////conventions.DefiningEventsAs(c => c.Namespace != null && c.Name.EndsWith("IntegrationEvent"));

            //// Configure the DI container.
            ////endpointConfiguration.UseContainer<AutofacBuilder>(customizations: customizations =>
            ////{
            ////    customizations.ExistingLifetimeScope(container);
            ////});

            //// Start the endpoint and register it with ASP.NET Core DI
            //endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            //#endregion

        }

        private void RegisterPublisherAndEndpoints(RoutingSettings<SqlServerTransport> routing)
        {
            // it means OrderAccepted message goees to Sender to be processed
           // routing.RouteToEndpoint(typeof(OrderAccepted), config.PublisherEndpoint);
            // it means Sender Endpoint publishes OrderSubmitted ecents
          //  routing.RegisterPublisher(typeof(OrderSubmitted).Assembly, config.PublisherEndpoint);




            routing.RouteToEndpoint(typeof(AlterationFinishedEvent), config.PublisherEndpoint); 
            //routing.RegisterPublisher(typeof(OrderSubmitted).Assembly, config.PublisherEndpoint);



        }

        private string GetRabbitConnectionString()
        {
            return $"host=127.0.0.1";
        }

        public void Dispose()
        {
            DisposeResources().Wait();

        }
        private async Task DisposeResources()
        {
            await endpointInstance.Stop().ConfigureAwait(false);
        }

    }
}
