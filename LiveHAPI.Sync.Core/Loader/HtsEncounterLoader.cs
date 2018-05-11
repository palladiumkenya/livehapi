using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Encounters;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;

namespace LiveHAPI.Sync.Core.Loader
{
    public class 
        HtsEncounterLoader : IHtsEncounterLoader
    {
        private readonly IClientPretestStageRepository _clientPretestStageRepository;
        private readonly IClientEncounterRepository _clientEncounterRepository;
        private readonly ISubscriberSystemRepository _subscriberSystemRepository;

        public HtsEncounterLoader(IClientEncounterRepository clientEncounterRepository, IClientPretestStageRepository clientPretestStageRepository, ISubscriberSystemRepository subscriberSystemRepository)
        {
            _clientEncounterRepository = clientEncounterRepository;
            _clientPretestStageRepository = clientPretestStageRepository;
            _subscriberSystemRepository = subscriberSystemRepository;
        }

        public ENCOUNTERS Load()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ENCOUNTERS> LoadAll()
        {
            throw new NotImplementedException();
        }

        public ENCOUNTERS Load(Guid clientId)
        {
            var sub = _subscriberSystemRepository.GetDefaultAsync().Result;

            var clientPretestStage = _clientPretestStageRepository.GetByClientId(clientId).FirstOrDefault();

            if (null != clientPretestStage)
            {
                // TODO: Generate placer_detail
                var pd = PLACER_DETAIL.Create(1, clientPretestStage.Id);

                var pretes = PRE_TEST.Create(clientPretestStage);
                

                var finalTesting = _clientEncounterRepository.GetFinalTesting(clientId).FirstOrDefault();
                SUMMARY summar = null;
                if (null != finalTesting)
                {
                    var stages = ClientFinalTestStage.Create(finalTesting, sub).FirstOrDefault();
                    summar = SUMMARY.Create(stages);
                }
                var testing = _clientEncounterRepository.GetTesting(clientId).FirstOrDefault();
                HIV_TESTS hv = null;
                if (null != testing)
                {
                    var stages = ClientTestingStage.Create(testing, sub);
                    hv = HIV_TESTS.Create(stages, summar);
                }

                var referall = _clientEncounterRepository.GetReferralLinkage(clientId).FirstOrDefault();
                NewReferral newrefferal=null;
                NewLinkage newlinkage=null;
                if (null != referall)
                {
                    var stage = ClientReferralStage.Create(referall,sub);
                     newrefferal = NewReferral.Create(stage);

                    var stage1 = ClientLinkageStage.Create(referall, sub);
                     newlinkage = NewLinkage.Create(stage1);
                }

                var tracing= _clientEncounterRepository.GetTracing(clientId).FirstOrDefault();
                List<NewTracing> newtracing=new List<NewTracing>();
                if (null != tracing)
                {
                    var stage = ClientTracingStage.Create(tracing, sub);
                    newtracing = NewTracing.Create(stage);
                }

                return ENCOUNTERS.Create(pd, pretes, hv, newrefferal, newtracing, newlinkage);
            }

            return null;
        }
    }
}