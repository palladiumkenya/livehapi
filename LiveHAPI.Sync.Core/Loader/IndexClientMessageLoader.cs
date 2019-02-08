using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using Serilog;

namespace LiveHAPI.Sync.Core.Loader
{
    public class IndexClientMessageLoader : IIndexClientMessageLoader
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IClientStageRepository _clientStageRepository;
        private readonly IClientPretestStageRepository _clientPretestStageRepository;
        private readonly IClientTestingStageExtractor _clientTestingStageExtractor;
        private readonly IClientFinalTestStageExtractor _clientFinalTestStageExtractor;
        private readonly IClientReferralStageExtractor _clientReferralStageExtractor;
        private readonly IClientTracingStageExtractor _clientTracingStageExtractor;
        private readonly IClientLinkageStageExtractor _clientLinkageStageExtractor;

        public IndexClientMessageLoader(IPracticeRepository practiceRepository,
            IClientStageRepository clientStageRepository, IClientPretestStageRepository clientPretestStageRepository,
            IClientTestingStageExtractor clientTestingStageExtractor,
            IClientFinalTestStageExtractor clientFinalTestStageExtractor,
            IClientReferralStageExtractor clientReferralStageExtractor,
            IClientTracingStageExtractor clientTracingStageExtractor,
            IClientLinkageStageExtractor clientLinkageStageExtractor)
        {
            _practiceRepository = practiceRepository;
            _clientStageRepository = clientStageRepository;
            _clientPretestStageRepository = clientPretestStageRepository;
            _clientTestingStageExtractor = clientTestingStageExtractor;
            _clientFinalTestStageExtractor = clientFinalTestStageExtractor;
            _clientReferralStageExtractor = clientReferralStageExtractor;
            _clientTracingStageExtractor = clientTracingStageExtractor;
            _clientLinkageStageExtractor = clientLinkageStageExtractor;
        }

        public async Task<IEnumerable<IndexClientMessage>> Load(Guid? htsClientId = null, params LoadAction[] actions)
        {
            var messages = new List<IndexClientMessage>();
            if (!actions.Any())
                actions = new[] {LoadAction.All};

            var facs = _practiceRepository.GetAllDefault().ToList();

            //  Set Facility
            var facility = facs.FirstOrDefault(x => x.IsDefault);
            if (null == facility)
                throw new Exception($"Default Faciltity Not found");

            //      MESSAGE_HEADER

            var facilityCode = facility.Code;
            var header = MESSAGE_HEADER.Create(facilityCode);

            //      NEWCLIENT

            var stagedIndexClients = _clientStageRepository.GetIndexClients();

            if (!htsClientId.IsNullOrEmpty())
                stagedIndexClients = stagedIndexClients.Where(x => x.ClientId == htsClientId);

            foreach (var stagedClient in stagedIndexClients)
            {

                header.UpdateMfl(stagedClient.SiteCode);

                #region PATIENT_IDENTIFICATION

                var pid = PATIENT_IDENTIFICATION.Create(stagedClient);

                #endregion

                ENCOUNTERS encounter = null;
                if (!actions.Contains(LoadAction.RegistrationOnly))
                {
                    var pretests = _clientPretestStageRepository.GetByClientId(stagedClient.ClientId).ToList();

                    //    PRETEST AND TESTING

                    if (pretests.Any())
                    {
                        var lastPretest = pretests.OrderByDescending(x => x.EncounterDate).FirstOrDefault();
                    
                        foreach (var pretest in pretests)
                        {
                            var pretestEncounter =
                                await CreatePretestEncounters(header, pid, stagedClient, pretest, actions);
                            messages.Add(pretestEncounter);
                        }

                        if (null != lastPretest)
                        {
                            var nonPretest =
                                await CreateNonPretestEncounters(header, pid, stagedClient, lastPretest, actions);
                            if (null != nonPretest)
                                messages.Add(nonPretest);
                        }
                    }
                    else
                    {
                        var registration = CreateRegistration(header, pid, stagedClient, encounter);
                        messages.Add(registration);
                    }
                }
                else
                {
                    messages.Add(new IndexClientMessage(header,
                        new List<NEWCLIENT> {NEWCLIENT.Create(pid, encounter)}, stagedClient.ClientId));
                }
            }

            return messages;
        }

        private IndexClientMessage CreateRegistration(MESSAGE_HEADER header,PATIENT_IDENTIFICATION pid, ClientStage stagedClient, ENCOUNTERS encounter)
        {
            return new IndexClientMessage(header,new List<NEWCLIENT> {NEWCLIENT.Create(pid, encounter)}, stagedClient.ClientId);
        }

        private async Task<IndexClientMessage> CreatePretestEncounters(MESSAGE_HEADER header,
            PATIENT_IDENTIFICATION pid, ClientStage stagedClient, ClientPretestStage pretest,
            params LoadAction[] actions)
        {

            ENCOUNTERS encounter = null;

            //  PLACER_DETAIL
            var placerDetail = PLACER_DETAIL.Create(pretest.UserId, pretest.Id);

            //  PRE_TEST
            PRE_TEST preTest = null;
            if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Pretest))
                preTest = PRE_TEST.Create(pretest);

            //  HIV_TESTS
            HIV_TESTS hivTests = null;
            if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Testing))
            {
                var allfinalTests = await _clientFinalTestStageExtractor.Extract(stagedClient.ClientId);
                var alltests = await _clientTestingStageExtractor.Extract();
            
                var finalTest = allfinalTests.Where(x => x.PretestEncounterId == pretest.Id).ToList()
                    .LastOrDefault();
            
                var tests = alltests.Where(x => x.PretestEncounterId == pretest.Id).ToList();

                if (null != finalTest && tests.Any())
                    hivTests = HIV_TESTS.Create(tests, finalTest);
            }

            // GET THE LAST ONE


            encounter = ENCOUNTERS.Create(placerDetail, preTest, hivTests, null, new List<NewTracing>(), null);

            return new IndexClientMessage(header,
                new List<NEWCLIENT> {NEWCLIENT.Create(pid, encounter)},stagedClient.ClientId);

        }

        private async Task<IndexClientMessage> CreateNonPretestEncounters(MESSAGE_HEADER header,PATIENT_IDENTIFICATION pid,ClientStage stagedClient,ClientPretestStage lastPretest, params LoadAction[] actions)
        {
            ENCOUNTERS encounter = null;
 
            //  PLACER_DETAIL
                    
            var lastplacerDetail = PLACER_DETAIL.Create(lastPretest.UserId, lastPretest.Id);
                    
            //  NewReferral
            NewReferral newReferral = null;
            if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Referral))
            {
                var allReferrals = await _clientReferralStageExtractor.Extract(stagedClient.ClientId);
                if (allReferrals.Any())
                {
                    var referrall = allReferrals.LastOrDefault();
                    newReferral = NewReferral.Create(referrall);
                }
            }

            //  NewTracing
            var newTracings = new List<NewTracing>();
            if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Tracing))
            {
                var allTracing = await _clientTracingStageExtractor.Extract(stagedClient.ClientId);
                if(allTracing.Any())
                    newTracings = NewTracing.Create(allTracing.ToList());
            }

            // NewLinkage
            NewLinkage newLinkage = null;
            if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Linkage))
            {
                var allLinkages = await _clientLinkageStageExtractor.Extract(stagedClient.ClientId);
                if (allLinkages.Any())
                {
                    var linkage = allLinkages.LastOrDefault();
                    newLinkage = linkage.HasData ? NewLinkage.Create(linkage) : null;
                }
            }

            if (null == newReferral && !newTracings.Any() && null == newLinkage)
                return null;
            
            encounter = ENCOUNTERS.Create(lastplacerDetail,null, null,newReferral,newTracings,newLinkage);

            return new IndexClientMessage(header,
                new List<NEWCLIENT> {NEWCLIENT.Create(pid, encounter)},stagedClient.ClientId);
        }

        public void Dispose()
        {
            _practiceRepository?.Dispose();
            _clientStageRepository?.Dispose();
            _clientPretestStageRepository?.Dispose();
            _clientTestingStageExtractor?.Dispose();
            _clientFinalTestStageExtractor?.Dispose();
            _clientReferralStageExtractor?.Dispose();
            _clientTracingStageExtractor?.Dispose();
            _clientLinkageStageExtractor?.Dispose();
        }
    }
}