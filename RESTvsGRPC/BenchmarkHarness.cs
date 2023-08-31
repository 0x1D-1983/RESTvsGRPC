using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Validators;
using ModelLibrary.Data;

namespace RESTvsGRPC
{
    [AsciiDocExporter]
    [CsvExporter]
    [HtmlExporter]
    public class BenchmarkHarness
    {
        [Params(100, 200)]
        public int IterationCount;

        readonly RESTClient restClient = new RESTClient();
        readonly GRPCClient grpcClient = new GRPCClient();

        [Benchmark]
        public async Task RestGetSmallPayloadAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await restClient.GetSmallPayloadAsync();
            }
        }

        [Benchmark]
        public async Task RestGetLargePayloadAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await restClient.GetLargePayloadAsync();
            }
        }

        [Benchmark]
        public async Task RestPostLargePayloadAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await restClient.PostLargePayloadAsync(MeteoriteLandingData.RestMeteoriteLandings);
            }
        }

        [Benchmark]
        public async Task GrpcGetSmallPayloadAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await grpcClient.GetSmallPayloadAsync();
            }
        }

        [Benchmark]
        public async Task GrpcStreamLargePayloadAsync()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            for (int i = 0; i < IterationCount; i++)
            {
                await grpcClient.StreamLargePayloadAsync(token);
            }
        }

        [Benchmark]
        public async Task GrpcGetLargePayloadAsListAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await grpcClient.GetLargePayloadAsListAsync();
            }
        }

        [Benchmark]
        public async Task GrpcPostLargePayloadAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await grpcClient.PostLargePayloadAsync(MeteoriteLandingData.GrpcMeteoriteLandingList);
            }
        }
    }

    public class AllowNonOptimized : ManualConfig
    {
        public AllowNonOptimized()
        {
            AddValidator(JitOptimizationsValidator.DontFailOnError);

            AddLogger(DefaultConfig.Instance.GetLoggers().ToArray());
            AddExporter(DefaultConfig.Instance.GetExporters().ToArray());
            AddColumnProvider(DefaultConfig.Instance.GetColumnProviders().ToArray());
        }
    }
}

