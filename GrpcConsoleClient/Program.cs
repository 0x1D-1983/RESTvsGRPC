using System.Threading.Tasks;
using Grpc.Net.Client;
using ModelLibrary.GRPC;
using static ModelLibrary.GRPC.MeteoriteLandingsService;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5178");

var client = new MeteoriteLandingsServiceClient(channel);

var reply = await client.GetVersionAsync(new EmptyRequest());
Console.WriteLine("API Version: " + reply.ApiVersion);

var reply2 = await client.GetLargePayloadAsListAsync(new EmptyRequest());
foreach(var impact in reply2.MeteoriteLandings)
{
    Console.WriteLine($"Meteor impact location: {impact.Reclat}, " +
        $"{impact.Reclong} - {impact.Year} - { impact.Name}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();