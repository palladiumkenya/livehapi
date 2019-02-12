using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers.Index;
using Newtonsoft.Json;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer.Index
{
    public class DemographicsWriter : ClientWriter<IndexClientMessage>, IDemographicsWriter
    {
        public DemographicsWriter(IRestClient restClient, IIndexClientMessageLoader loader,
            IClientStageRepository clientStageRepository)
            : base(restClient, loader, clientStageRepository)
        {
        }

        public override Task<IEnumerable<SynchronizeClientsResponse>> Write(params LoadAction[] actions)
        {
            return WriteMessage("api/Hts/demographics", actions);
        }
          private async Task<IEnumerable<SynchronizeClientsResponse>> WriteMessage(string endpoint,
            params LoadAction[] actions)
        {
            _errors = new List<ErrorResponse>();
            var results = new List<SynchronizeClientsResponse>();
            var htsClients = await _loader.Load(null, actions);
            foreach (var htsClient in htsClients)
            {
               
                SynchronizeClientsResponse result = null;

                try
                {
                    var msg = JsonConvert.SerializeObject(htsClient, Formatting.Indented);
                    _messages.Add(msg);

                    var response = await _restClient.Client.PostAsJsonAsync(endpoint, htsClient.GetDemographicMessage());

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsJsonAsync<SynchronizeClientsResponse>();
                        results.Add(result);
                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, SyncStatus.SentSuccess);
                    }
                    else
                    {
                        try
                        {
                            result = await response.Content.ReadAsJsonAsync<SynchronizeClientsResponse>();
                        }
                        catch
                        {
                            Log.Error(new string('+', 40));
                            Log.Error(new string('+', 40));
                            Log.Error($"Unkown server Error!");
                            var error = await response.Content.ReadAsStringAsync();
                            Log.Error(error);
                            Log.Error(new string('+', 40));
                            Log.Error(new string('+', 40));
                        }

                        Log.Debug(new string('_', 50));
                        Log.Error($"\n{msg}\n");
                        Log.Debug(new string('-', 50));

                        if (null != result)
                        {
                            _errors = result?.Errors;
                            throw new Exception($"Error processing request: {result?.ErrorMessage}");
                        }
                        else
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, $"error posting to endpint [{endpoint}] for {nameof(IndexClientMessage)}");
                    _errors.Add(new ErrorResponse($"{endpoint} || {e.Message}"));
                    _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, SyncStatus.SentFail, e.Message);
                }
            }

            return results;
        }
    }
}