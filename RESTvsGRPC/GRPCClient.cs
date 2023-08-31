using System.Threading.Tasks;
using Grpc.Net.Client;
using ModelLibrary.GRPC;
using static ModelLibrary.GRPC.MeteoriteLandingsService;

namespace RESTvsGRPC
{
    public class GRPCClient
    {
        private readonly GrpcChannel channel;
        private readonly MeteoriteLandingsServiceClient client;

        private const string GRPC_API_BASE_URL = "http://localhost:5178";

        public GRPCClient()
        {
            channel = GrpcChannel.ForAddress($"{GRPC_API_BASE_URL}",
                new GrpcChannelOptions { UnsafeUseInsecureChannelCallCredentials = true});
            client = new MeteoriteLandingsServiceClient(channel);
        }

        public async Task<string> GetSmallPayloadAsync()
        {
            return (await client.GetVersionAsync(new EmptyRequest())).ApiVersion;
        }

        public async Task<List<MeteoriteLanding>> StreamLargePayloadAsync(CancellationToken cancellationToken)
        {
            List<MeteoriteLanding> meteoriteLandings = new List<MeteoriteLanding>();

            var response = client.GetLargePayload(new EmptyRequest()).ResponseStream;

            while (await response.MoveNext(cancellationToken))
            {
                meteoriteLandings.Add(response.Current);
            }

            return meteoriteLandings;
        }

        public async Task<IList<MeteoriteLanding>> GetLargePayloadAsListAsync()
        {
            return (await client.GetLargePayloadAsListAsync(new EmptyRequest())).MeteoriteLandings;
        }

        public async Task<string> PostLargePayloadAsync(MeteoriteLandingList meteoriteLandings)
        {
            return (await client.PostLargePayloadAsync(meteoriteLandings)).Status;
        }
    }
}

