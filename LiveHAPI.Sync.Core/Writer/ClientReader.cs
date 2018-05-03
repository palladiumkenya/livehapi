using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer
{
    public abstract class ClientWriter<T>:IClientWriter<T>
    {
        private readonly HttpClient _httpClient;
        protected ClientWriter(IRestClient restClient)
        {
            _httpClient = restClient.Client;
        }

        public Task<IEnumerable<T>> Write()
        {
            return Write($"api/{typeof(T).Name}");
        }

        protected async Task<IEnumerable<T>> Write(string endpoint)
        {
            var data=new List<T>();
            var result = new List<T>();
            try
            {
                var response = await _httpClient.PostAsJsonAsync<List<T>>(endpoint,data);
                result = await response.Content.ReadAsJsonAsync<List<T>>();
            }
            catch (Exception e)
            {
                Log.Error($"error posting to endpint [{endpoint}] for {typeof(T).Name}");
                Log.Error($"{e}");
            }

            return result;
        }
    }
}