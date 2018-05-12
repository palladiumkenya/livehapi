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
using Newtonsoft.Json.Serialization;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer
{
    public abstract class ClientWriter<T> : IClientWriter<T>
    {
        private readonly HttpClient _httpClient;
        private readonly IMessageLoader<T> _loader;
        private string _message;
        private List<SendError> _errors;

        protected ClientWriter(IRestClient restClient, IMessageLoader<T> loader)
        {
            _httpClient = restClient.Client;
            _loader = loader;
        }

        public string Message => _message;

        public List<SendError> Errors => _errors;

        public virtual Task<IEnumerable<SynchronizeClientsResponse>> Write()
        {
            return Write($"api/{typeof(T).Name}");
        }

        protected async Task<IEnumerable<SynchronizeClientsResponse>> Write(string endpoint)
        {
            _errors =new List<SendError>();
            var results = new List<SynchronizeClientsResponse>();
            var htsClients =await _loader.Load();
            foreach (var htsClient in htsClients)
            {
                _message = JsonConvert.SerializeObject(htsClient);
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(endpoint, htsClient);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsJsonAsync<SynchronizeClientsResponse>();
                    results.Add(result);
                }
                catch (Exception e)
                {
                    Log.Error($"error posting to endpint [{endpoint}] for {typeof(T).Name}");
                    Log.Error($"{e}");
                    _errors.Add(new SendError()
                    {
                        ErrorMessage =e.Message
                    });
                }

            }

            return results;
        }
    }
}