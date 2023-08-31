using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelLibrary.REST
{
    public class MeteoriteLanding
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("fall")]
        public string Fall { get; set; }

        [JsonPropertyName("geolocation")]
        public GeoLocation GeoLocation { get; set; }

        [JsonPropertyName("mass")]
        public double Mass { get; set; }

        [JsonPropertyName("nametype")]
        public string NameType { get; set; }

        [JsonPropertyName("recclass")]
        public string RecClass { get; set; }

        [JsonPropertyName("reclat")]
        public double RecLAT { get; set; }

        [JsonPropertyName("reclong")]
        public double RecLONG { get; set; }

        [JsonPropertyName("year")]
        public DateTime Year { get; set; }
    }
}
