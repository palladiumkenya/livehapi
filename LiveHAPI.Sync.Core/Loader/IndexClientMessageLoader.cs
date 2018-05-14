using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
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

        public async Task<IEnumerable<IndexClientMessage>> Load(params LoadAction[] actions)
        {
            var messages = new List<IndexClientMessage>();
            if (!actions.Any())
                actions = new[] {LoadAction.All};

            //  Set Facility
            var facility = _practiceRepository.GetDefault();
            if (null == facility)
                throw new Exception($"Default Faciltity Not found");

            //      MESSAGE_HEADER

            var facilityCode = facility.Code;
            var header = MESSAGE_HEADER.Create(facilityCode);

            //      NEWCLIENT

            var stagedIndexClients = _clientStageRepository.GetIndexClients();

            foreach (var stagedClient in stagedIndexClients)
            {

                #region PATIENT_IDENTIFICATION

                var pid = PATIENT_IDENTIFICATION.Create(stagedClient);

                #endregion


                ENCOUNTERS encounter = null;
                if (!actions.Contains(LoadAction.RegistrationOnly))
                {
                    #region ENCOUNTERS

                    var pretests = _clientPretestStageRepository.GetByClientId(stagedClient.ClientId).ToList();
                    var pretest = pretests.OrderByDescending(x => x.EncounterDate).FirstOrDefault();

                   //  PLACER_DETAIL
                    var placerDetail = PLACER_DETAIL.Create(1, pretest.Id);

                    //  PRE_TEST
                    PRE_TEST preTest = null;
                    if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Pretest))
                        preTest = PRE_TEST.Create(pretest);

                    //  HIV_TESTS
                    HIV_TESTS hivTests = null;
                    if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Testing))
                    {
                        var allfinalTests = await _clientFinalTestStageExtractor.Extract();
                        var finalTest = allfinalTests.ToList().LastOrDefault();
                        var alltests = await _clientTestingStageExtractor.Extract();

                        hivTests = HIV_TESTS.Create(alltests.ToList(), finalTest);
                    }

                    //  NewReferral
                    NewReferral newReferral = null;
                    if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Referral))
                    {
                        var allReferrals = await _clientReferralStageExtractor.Extract();
                        var referrall = allReferrals.LastOrDefault();
                        newReferral = NewReferral.Create(referrall);
                    }

                    //  NewTracing
                    List<NewTracing> newTracings = new List<NewTracing>();
                    if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Tracing))
                    {
                        var allTracing = await _clientTracingStageExtractor.Extract();
                        newTracings = NewTracing.Create(allTracing.ToList());
                    }

                    // NewLinkage
                    NewLinkage newLinkage = null;
                    if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.Linkage))
                    {
                        var allLinkages = await _clientLinkageStageExtractor.Extract();
                        var linkage = allLinkages.LastOrDefault();

                        newLinkage = NewLinkage.Create(linkage);
                    }

                    encounter = ENCOUNTERS.Create(placerDetail, preTest, hivTests, newReferral, newTracings,
                        newLinkage);

                    #endregion
                }

                messages.Add(new IndexClientMessage(header, new List<NEWCLIENT> {NEWCLIENT.Create(pid, encounter)}));
            }

            return messages;
        }
    }
}