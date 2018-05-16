using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
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
        private List<ErrorResponse> _errors=new List<ErrorResponse>();

        protected ClientWriter(IRestClient restClient, IMessageLoader<T> loader)
        {
            _httpClient = restClient.Client;
            _loader = loader;
        }

        public string Message => _message;

        public List<ErrorResponse> Errors => _errors;

        public virtual Task<IEnumerable<SynchronizeClientsResponse>> Write(params LoadAction[] actions)
        {
            return Write($"api/{typeof(T).Name}",actions);
        }

        protected async Task<IEnumerable<SynchronizeClientsResponse>> Write(string endpoint,params LoadAction[] actions)
        {
            _errors =new List<ErrorResponse>();
            var results = new List<SynchronizeClientsResponse>();
            var htsClients =await _loader.Load(actions);
            foreach (var htsClient in htsClients)
            {
                _message = JsonConvert.SerializeObject(htsClient);
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(endpoint, htsClient);
                    var result = await response.Content.ReadAsJsonAsync<SynchronizeClientsResponse>();
                    if (response.IsSuccessStatusCode)
                    {
                        results.Add(result);
                    }
                    else
                    {
                        _errors = result.ErrorResponses;
                        throw new Exception($"Error processing request: {string.Join(",",_errors)}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"error posting to endpint [{endpoint}] for {typeof(T).Name}");
                    Log.Error($"{e}");
                    _errors.Add(new ErrorResponse($"{endpoint} || {e.Message}"));
                }

            }

            return results;
        }
    }
}