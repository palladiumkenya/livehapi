using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Exchange.Messages.Familiy;
using LiveHAPI.Sync.Core.Exchange.Messages.Index;
using LiveHAPI.Sync.Core.Exchange.Messages.Partner;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers.Partner;
using LiveHAPI.Sync.Core.Model;
using Newtonsoft.Json;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer.Partner
{
    public class PartnerWriter : ClientWriter<PartnerClientMessage>, IPartnerWriter
    {
        private List<SynchronizeClientsResponse> _results;

        public PartnerWriter(IRestClient restClient, IPartnerClientMessageLoader loader,
            IClientStageRepository clientStageRepository)
            : base(restClient, loader, clientStageRepository)
        {
        }

        public override Task<IEnumerable<SynchronizeClientsResponse>> Write(params LoadAction[] actions)
        {
            return WriteMessage("api/Hts", actions);
        }

        private async Task<IEnumerable<SynchronizeClientsResponse>> WriteMessage(string endpoint,
            params LoadAction[] actions)
        {
            _errors = new List<ErrorResponse>();
            _results = new List<SynchronizeClientsResponse>();
            var htsClients = await _loader.Load(null, actions);
            foreach (var htsClient in htsClients)
            {
                try
                {
                    // LoadAction.RegistrationOnly

                    var demographicsReport =
                        await SendMessage($"{endpoint}/partnerdemographics", htsClient.ClientId,
                            GetMessage<PartnerMessage>(htsClient));

                    SyncReport screeningReport = null;
                    SyncReport tracingReport = null;

                    if (null != demographicsReport && demographicsReport.IsSuccess)
                    {

                        // LoadAction.ContactScreenig

                         screeningReport =
                            await SendMessage($"{endpoint}/partnerScreening", htsClient.ClientId,
                                GetMessage<PartnerScreening>(htsClient));

                        // LoadAction.ContactTracing

                         tracingReport =
                            await SendMessage($"{endpoint}/partnerTracing", htsClient.ClientId,
                                GetMessage<PartnerTracing>(htsClient));
                    }

                    if (null != demographicsReport)
                    {
                        if (demographicsReport.HasResponse)
                            _results.Add(demographicsReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, demographicsReport.Status,
                            demographicsReport.ExceptionInfo);
                    }
                    if (null != screeningReport)
                    {
                        if (screeningReport.HasResponse)
                            _results.Add(screeningReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, screeningReport.Status,
                            screeningReport.ExceptionInfo);
                    }
                    if (null != tracingReport)
                    {
                        if (tracingReport.HasResponse)
                            _results.Add(tracingReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, tracingReport.Status,
                            tracingReport.ExceptionInfo);
                    }
                }
                catch (Exception e)
                {
                    _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, SyncStatus.SentFail, e.Message);
                }
            }

            return _results;
        }

        private T GetMessage<T>(PartnerClientMessage message) where T : ClientMessage
        {
            T clientMessage = default(T);

            try
            {
                if (typeof(T) == typeof(PartnerMessage))
                    clientMessage = message.GetDemographicMessage() as T;

                if (typeof(T) == typeof(PartnerScreening))
                    clientMessage = message.GetScreeningMessage() as  T;

                if (typeof(T) == typeof(PartnerTracing))
                    clientMessage = message.GetTracingMessage() as T;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error generating {typeof(T).Name}");
                clientMessage = null;
            }

            return clientMessage;
        }

        private async Task<SyncReport> SendMessage<T>(string endpoint, Guid clientId, T message)
        {
            SyncReport report = null;
            SynchronizeClientsResponse result = null;
            string jsonMessage = string.Empty;

            if (null == message)
                return null;

            try
            {
                var response = await _restClient.Client.PostAsJsonAsync(endpoint, message);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsJsonAsync<SynchronizeClientsResponse>();
                    _results.Add(result);
                    report = SyncReport.GenerateSuccess(clientId, endpoint, result);
                }
                else
                {
                    jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);

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
                    Log.Error($"{endpoint}");
                    Log.Error($"\n{jsonMessage}\n");
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
                report = SyncReport.GenerateFail(clientId, endpoint, result, jsonMessage, e, e.Message);
            }

            return report;
        }

    }

}
