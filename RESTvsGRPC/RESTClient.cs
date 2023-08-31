using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ModelLibrary.REST;

namespace RESTvsGRPC
{
    public class RESTClient
    {
        private static readonly HttpClient client = new HttpClient();

        private const string REST_API_BASE_URL = "http://localhost:5204";

        public async Task<string> GetSmallPayloadAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetStringAsync($"{REST_API_BASE_URL}/api/MeteoriteLandings");
        }

        public async Task<List<MeteoriteLanding>> GetLargePayloadAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string meteoriteLandingsString = await client.GetStringAsync($"{REST_API_BASE_URL}/api/MeteoriteLandings/LargePayload");

            return JsonSerializer.Deserialize<List<MeteoriteLanding>>(meteoriteLandingsString);
        }

        public async Task<string> PostLargePayloadAsync(List<MeteoriteLanding> meteoriteLandings)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync($"{REST_API_BASE_URL}/api/MeteoriteLandings/LargePayload", meteoriteLandings);

            return await response.Content.ReadAsStringAsync();
        }
    }
}

