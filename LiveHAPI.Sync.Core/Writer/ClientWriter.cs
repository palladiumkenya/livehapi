using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer
{
    public abstract class ClientWriter<T> : IClientWriter<T> where T : ClientMessage
    {
        private readonly HttpClient _httpClient;
        private readonly IMessageLoader<T> _loader;
        private List<string> _messages=new List<string>();
        private List<ErrorResponse> _errors=new List<ErrorResponse>();
        private readonly IClientStageRepository _clientStageRepository;

        protected ClientWriter(IRestClient restClient, IMessageLoader<T> loader, IClientStageRepository clientStageRepository)
        {
            _httpClient = restClient.Client;
            _loader = loader;
            _clientStageRepository = clientStageRepository;
        }

        public List<string> Messages => _messages;

        public List<ErrorResponse> Errors => _errors;

        public virtual Task<IEnumerable<SynchronizeClientsResponse>> Write(params LoadAction[] actions)
        {
            return Write($"api/{typeof(T).Name}",actions);
        }

        protected async Task<IEnumerable<SynchronizeClientsResponse>> Write(string endpoint,params LoadAction[] actions)
        {
            _errors =new List<ErrorResponse>();
            var results = new List<SynchronizeClientsResponse>();
            var htsClients =await _loader.Load(null, actions);
            foreach (var htsClient in htsClients)
            {
                _messages.Add(JsonConvert.SerializeObject(htsClient));
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(endpoint, htsClient);
                    var result = await response.Content.ReadAsJsonAsync<SynchronizeClientsResponse>();
                    if (response.IsSuccessStatusCode)
                    {
                        results.Add(result);
                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId,SyncStatus.SentSuccess);
                    }
                    else
                    {
                        _errors = result.Errors;
                        throw new Exception($"Error processing request: {string.Join(",",_errors)}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"error posting to endpint [{endpoint}] for {typeof(T).Name}");
                    Log.Error($"{e}");
                    _errors.Add(new ErrorResponse($"{endpoint} || {e.Message}"));
                    _clientStageRepository.UpdateSyncStatus(htsClient.ClientId,SyncStatus.SentFail,e.Message);
                }
            }
            return results;
        }
    }
}