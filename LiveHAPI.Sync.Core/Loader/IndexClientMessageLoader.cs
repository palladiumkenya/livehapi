using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
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

        public IndexClientMessageLoader(IPracticeRepository practiceRepository, IClientStageRepository clientStageRepository, IClientPretestStageRepository clientPretestStageRepository, IClientTestingStageExtractor clientTestingStageExtractor, IClientFinalTestStageExtractor clientFinalTestStageExtractor, IClientReferralStageExtractor clientReferralStageExtractor, IClientTracingStageExtractor clientTracingStageExtractor, IClientLinkageStageExtractor clientLinkageStageExtractor)
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

        public async Task<IEnumerable<IndexClientMessage>> Load()
        {
            var messages=new List<IndexClientMessage>();

            //  Set Facility
            var facility = _practiceRepository.GetDefault();
            if (null == facility)
                throw new Exception($"Default Faciltity Not found");

            //      MESSAGE_HEADER

            var facilityCode = facility.Code;
            var header = MESSAGE_HEADER.Create(facilityCode);

            //      NEWCLIENT

            var stagedIndexClients = _clientStageRepository.GetAll();

            foreach (var stagedClient in stagedIndexClients)
            {

                #region PATIENT_IDENTIFICATION

                var pid = PATIENT_IDENTIFICATION.Create(stagedClient);

                #endregion


                #region ENCOUNTERS
                
                var pretests = _clientPretestStageRepository.GetByClientId(stagedClient.ClientId).ToList();
                var pretest = pretests.OrderByDescending(x => x.EncounterDate).FirstOrDefault();

                //  PLACER_DETAIL
                var pd = PLACER_DETAIL.Create(1, pretest.Id);

                //  PRE_TEST
                var pr = PRE_TEST.Create(pretest);

                //  HIV_TESTS
                var allfinalTests = await _clientFinalTestStageExtractor.Extract();
                var finalTest = allfinalTests.ToList().FirstOrDefault();
                var alltests = await _clientTestingStageExtractor.Extract();

                var ht = HIV_TESTS.Create(alltests.ToList(), finalTest);
                //  NewReferral
                var allReferrals = await _clientReferralStageExtractor.Extract();
                var referrall = allReferrals.FirstOrDefault();
                var nr = NewReferral.Create(referrall);

                //  NewTracing
                var allTracing = await _clientTracingStageExtractor.Extract();
                var tr = NewTracing.Create(allTracing.ToList());

                // NewLinkage
                var allLinkages = await _clientLinkageStageExtractor.Extract();
                var linkage = allLinkages.FirstOrDefault();

                var ln = NewLinkage.Create(linkage);

                var en = ENCOUNTERS.Create(pd, pr, ht, nr, tr, ln);
                #endregion

                messages.Add(new IndexClientMessage(header,new List<NEWCLIENT>{NEWCLIENT.Create(pid,en)}));
            }

            return new List<IndexClientMessage>();
        }
    }
}