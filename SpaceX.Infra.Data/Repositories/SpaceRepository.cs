using Newtonsoft.Json;
using SpaceX.Business;
using SpaceX.Business.Entities;
using SpaceX.Infra.Data.SerializerContractResolvers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpaceX.Infra.Data
{
    public class SpaceRepository : ISpaceRepository,IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

        public SpaceRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.spacexdata.com");

            serializerSettings.ContractResolver = new LaunchSerializerResolver();
        }

        public async Task<IEnumerable<Launch>> GetLastLaunch()
        {
            List<Launch> launches = new List<Launch>();
            var response = await _httpClient.GetAsync("v3/launches/latest");

            if(response.IsSuccessStatusCodeErro)
            {
                dynamic responseContent = response.Content.ReadAsStringAsync();

                dynamic launch = JsonConvert.DeserializeObject<Launch>(responseContent.Result, serializerSettings);
                launches.Add(launch);
                return launches;
            }
            else
                throw new HttpRequestException($"Falha ao recuperar lançamentos da API. Status retornado: {response.StatusCode}");
        }

        public async Task<IEnumerable<Launch>> GetNextLaunch()
        {
            List<Launch> launches = new List<Launch>();
            var response = await _httpClient.GetAsync("v3/launches/next");

            if (response.IsSuccessStatusCode)
            {
                dynamic responseContent = response.Content.ReadAsStringAsync();

                dynamic launch = JsonConvert.DeserializeObject<Launch>(responseContent.Result, serializerSettings);
                launches.Add(launch);
                return launches;
            }
            else
                throw new HttpRequestException($"Falha ao recuperar lançamentos da API. Status retornado: {response.StatusCode}");
        }

        public async Task<IEnumerable<Launch>> GetPastLaunches()
        {
            var response = await _httpClient.GetAsync("v3/launches/past");

            if (response.IsSuccessStatusCode)
            {
                dynamic responseContent = response.Content.ReadAsStringAsync();

                dynamic launches = JsonConvert.DeserializeObject<IEnumerable<Launch>>(responseContent.Result, serializerSettings);
                return launches;
            }
            else
                throw new HttpRequestException($"Falha ao recuperar lançamentos da API. Status retornado: {response.StatusCode}");
        }

        public async Task<IEnumerable<Launch>> GetUpcomingLaunches()
        {
            var response = await _httpClient.GetAsync("v3/launches/upcoming");

            if (response.IsSuccessStatusCode)
            {
                dynamic responseContent = response.Content.ReadAsStringAsync();

                dynamic launches = JsonConvert.DeserializeObject<IEnumerable<Launch>>(responseContent.Result, serializerSettings);
                return launches;
            }
            else
                throw new HttpRequestException($"Falha ao recuperar lançamentos da API. Status retornado: {response.StatusCode}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
