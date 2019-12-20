using NServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Title = "ProposeNewRebooking";

        var endpointConfiguration = new EndpointConfiguration("ProposeNewRebooking");
        endpointConfiguration.UseTransport<LearningTransport>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendFailedMessagesTo("error");

        endpointConfiguration.AuditSagaStateChanges("Particular.ServiceControl");
        endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("Press enter to exit");
        Console.ReadLine();

        await endpointInstance.Stop().ConfigureAwait(false);
    }
}