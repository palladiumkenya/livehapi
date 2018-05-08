using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers;
using Newtonsoft.Json;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer
{
    public abstract class ClientWriter<T> : IClientWriter<T>
    {
        private readonly HttpClient _httpClient;
        private readonly ILoader<T> _loader;
        private string _message;

        protected ClientWriter(IRestClient restClient, ILoader<T> loader)
        {
            _httpClient = restClient.Client;
            _loader = loader;
        }

        public string Message => _message;

        public virtual Task<IEnumerable<SynchronizeClientsResponse>> Write()
        {
            return Write($"api/{typeof(T).Name}");
        }

        protected async Task<IEnumerable<SynchronizeClientsResponse>> Write(string endpoint)
        {
            var data =  _loader.Load();
            _message = JsonConvert.SerializeObject(data);

            var result = new List<SynchronizeClientsResponse>();
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, data);
                result = await response.Content.ReadAsJsonAsync<List<SynchronizeClientsResponse>>();
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