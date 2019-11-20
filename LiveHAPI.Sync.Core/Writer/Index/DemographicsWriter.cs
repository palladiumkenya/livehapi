using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Exchange.Messages.Index;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers.Index;
using LiveHAPI.Sync.Core.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer.Index
{
    public class DemographicsWriter : ClientWriter<IndexClientMessage>, IDemographicsWriter
    {
        private List<SynchronizeClientsResponse> _results;

        public DemographicsWriter(IRestClient restClient, IIndexClientMessageLoader loader,
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

                    SyncReport pretestReport = null;
                    SyncReport htstestReport = null;
                    SyncReport referrallReport= null;
                    SyncReport tracingReport= null;
                    SyncReport linkageReport = null;

                    var demographicsReport =
                        await SendMessage($"{endpoint}/demographics", htsClient.ClientId,
                            GetMessage<DemographicMessage>(htsClient));

                    if (null != demographicsReport && demographicsReport.IsSuccess)
                    {
                        // LoadAction.Pretest

                         pretestReport =
                            await SendMessage($"{endpoint}/htsPretest", htsClient.ClientId,
                                GetMessage<PretestMessage>(htsClient));

                        if (null != pretestReport && pretestReport.IsSuccess)
                        {

                            // LoadAction.Testing

                             htstestReport =
                                await SendMessage($"{endpoint}/htsTests", htsClient.ClientId,
                                    GetMessage<TestsMessage>(htsClient));
                        }

                        // LoadAction.Referral

                         referrallReport =
                            await SendMessage($"{endpoint}/htsReferral", htsClient.ClientId,
                                GetMessage<ReferralMessage>(htsClient));

                        // LoadAction.Tracing

                         tracingReport =
                            await SendMessage($"{endpoint}/htsTracing", htsClient.ClientId,
                                GetMessage<TracingMessage>(htsClient));

                        // LoadAction.Linkage

                         linkageReport =
                            await SendMessage($"{endpoint}/htsLinkage", htsClient.ClientId,
                                GetMessage<LinkageMessage>(htsClient));

                    }

                    if (null != demographicsReport)
                    {
                        if (demographicsReport.HasResponse)
                            _results.Add(demographicsReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, demographicsReport.Status,
                            demographicsReport.ExceptionInfo);
                    }

//////////
                    if (null != pretestReport)
                    {
                        if (pretestReport.HasResponse)
                            _results.Add(pretestReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, pretestReport.Status,
                            pretestReport.ExceptionInfo);
                    }

                    if (null != htstestReport)
                    {
                        if (htstestReport.HasResponse)
                            _results.Add(htstestReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, htstestReport.Status,
                            htstestReport.ExceptionInfo);
                    }

                    if (null != referrallReport)
                    {
                        if (referrallReport.HasResponse)
                            _results.Add(referrallReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, referrallReport.Status,
                            referrallReport.ExceptionInfo);
                    }
                    if (null != tracingReport)
                    {
                        if (tracingReport.HasResponse)
                            _results.Add(tracingReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, tracingReport.Status,
                            tracingReport.ExceptionInfo);
                    }

                    if (null != linkageReport)
                    {
                        if (linkageReport.HasResponse)
                            _results.Add(linkageReport.Response);

                        _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, linkageReport.Status,
                            linkageReport.ExceptionInfo);
                    }


                }
                catch (Exception e)
                {
                    _clientStageRepository.UpdateSyncStatus(htsClient.ClientId, SyncStatus.SentFail, e.Message);
                }
            }

            return _results;
        }

        private T GetMessage<T>(IndexClientMessage message) where T : ClientMessage
        {
            T clientMessage = default(T);

            try
            {

                if (typeof(T) == typeof(DemographicMessage))
                    clientMessage = message.GetDemographicMessage() as T;

                if (typeof(T) == typeof(PretestMessage))
                    clientMessage = message.GetPretestMessage() as  T;

                if (typeof(T) == typeof(TestsMessage))
                    clientMessage = message.GetHtsTestMessage() as T;

                if (typeof(T) == typeof(ReferralMessage))
                    clientMessage = message.GetReferralMessage() as T;

                if (typeof(T) == typeof(LinkageMessage))
                    clientMessage = message.GetLinkageMessage() as T;

                if (typeof(T) == typeof(TracingMessage))
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
