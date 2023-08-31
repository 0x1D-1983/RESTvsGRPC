using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelLibrary.Data
{
    public static class MeteoriteLandingData
    {
        static string meteoriteLandingsJson;
        public static string MeteoriteLandingsJson
        {
            get
            {
                if (meteoriteLandingsJson == null)
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceStream = assembly.GetManifestResourceStream("ModelLibrary.Data.MeteoriteLandings.json");

                    using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
                    {
                        meteoriteLandingsJson = reader.ReadToEnd();
                    }
                }
                return meteoriteLandingsJson;
            }
        }

        static List<REST.MeteoriteLanding> restMeteoriteLandings;
        public static List<REST.MeteoriteLanding> RestMeteoriteLandings
        {
            get
            {
                if (restMeteoriteLandings == null)
                {
                    var options = new JsonSerializerOptions()
                    {
                        NumberHandling = JsonNumberHandling.AllowReadingFromString |
     JsonNumberHandling.WriteAsString
                    };

                    restMeteoriteLandings = JsonSerializer.Deserialize<List<REST.MeteoriteLanding>>(
                        MeteoriteLandingsJson, options);
                }
                return restMeteoriteLandings;
            }
        }

        static List<GRPC.MeteoriteLanding> grpcMeteoriteLandings;
        public static List<GRPC.MeteoriteLanding> GrpcMeteoriteLandings
        {
            get
            {
                if (grpcMeteoriteLandings == null)
                {
                    var options = new JsonSerializerOptions()
                    {
                        NumberHandling = JsonNumberHandling.AllowReadingFromString |
     JsonNumberHandling.WriteAsString,
                        WriteIndented = true
                    };

                    options.Converters.Add(new ProtobufJsonConvertor());

                    grpcMeteoriteLandings = JsonSerializer.Deserialize<List<GRPC.MeteoriteLanding>>(
                        MeteoriteLandingsJson, options);
                }
                return grpcMeteoriteLandings;
            }
        }

        static GRPC.MeteoriteLandingList grpcMeteoriteLandingList;
        public static GRPC.MeteoriteLandingList GrpcMeteoriteLandingList
        {
            get
            {
                if (grpcMeteoriteLandingList == null)
                {
                    grpcMeteoriteLandingList = new GRPC.MeteoriteLandingList();
                    grpcMeteoriteLandingList.MeteoriteLandings.AddRange(GrpcMeteoriteLandings);
                }
                return grpcMeteoriteLandingList;
            }
        }
    }
}
