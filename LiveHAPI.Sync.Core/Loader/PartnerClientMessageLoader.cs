using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;
using LiveHAPI.Sync.Core.Exchange.Partner;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using Serilog;

namespace LiveHAPI.Sync.Core.Loader
{
    public class PartnerClientMessageLoader : IPartnerClientMessageLoader
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IClientStageRepository _clientStageRepository;
        private readonly IClientPretestStageRepository _clientPretestStageRepository;
        private readonly IClientStageRelationshipRepository _clientStageRelationshipRepository;
        private readonly IContactsEncounterRepository _contactsEncounterRepository;
        private readonly IClientTestingStageExtractor _clientTestingStageExtractor;
        private readonly IClientFinalTestStageExtractor _clientFinalTestStageExtractor;
        private readonly IClientReferralStageExtractor _clientReferralStageExtractor;
        private readonly IClientTracingStageExtractor _clientTracingStageExtractor;
        private readonly IClientLinkageStageExtractor _clientLinkageStageExtractor;
        private readonly IClientPartnerScreeningStageExtractor _clientPartnerScreeningStageExtractor;
        private readonly IClientPartnerTracingStageExtractor _clientPartnerTracingStageExtractor;

        public PartnerClientMessageLoader(IPracticeRepository practiceRepository,
            IClientStageRepository clientStageRepository, IClientPretestStageRepository clientPretestStageRepository,
            IClientTestingStageExtractor clientTestingStageExtractor,
            IClientFinalTestStageExtractor clientFinalTestStageExtractor,
            IClientReferralStageExtractor clientReferralStageExtractor,
            IClientTracingStageExtractor clientTracingStageExtractor,
            IClientLinkageStageExtractor clientLinkageStageExtractor,
            IClientStageRelationshipRepository clientStageRelationshipRepository,
            IContactsEncounterRepository contactsEncounterRepository,
            IClientPartnerScreeningStageExtractor clientPartnerScreeningStageExtractor,
            IClientPartnerTracingStageExtractor clientPartnerTracingStageExtractor)
        {
            _practiceRepository = practiceRepository;
            _clientStageRepository = clientStageRepository;
            _clientPretestStageRepository = clientPretestStageRepository;
            _clientTestingStageExtractor = clientTestingStageExtractor;
            _clientFinalTestStageExtractor = clientFinalTestStageExtractor;
            _clientReferralStageExtractor = clientReferralStageExtractor;
            _clientTracingStageExtractor = clientTracingStageExtractor;
            _clientLinkageStageExtractor = clientLinkageStageExtractor;
            _clientStageRelationshipRepository = clientStageRelationshipRepository;
            _contactsEncounterRepository = contactsEncounterRepository;
            _clientPartnerScreeningStageExtractor = clientPartnerScreeningStageExtractor;
            _clientPartnerTracingStageExtractor = clientPartnerTracingStageExtractor;
        }

        public async Task<IEnumerable<PartnerClientMessage>> Load()
        {
            var messages = new List<PartnerClientMessage>();

            //  Set Facility
            var facility = _practiceRepository.GetDefault();
            if (null == facility)
                throw new Exception($"Default Faciltity Not found");

            //      MESSAGE_HEADER

            var facilityCode = facility.Code;
            var header = MESSAGE_HEADER.Create(facilityCode);

            //      NEWCLIENT

            var fams = _clientStageRelationshipRepository.GetAll(x => !x.IsPartner);

            foreach (var fam in fams)
            {
                var stagedClient = _clientStageRepository.Get(fam.SecondaryClientId);
                if (null != stagedClient && !stagedClient.IsIndex)
                {
                    #region PATIENT_IDENTIFICATION

                    var pid = PARTNER_Partner_PATIENT_IDENTIFICATION.Create(stagedClient, fam.IndexClientId,fam.Relation);

                    #endregion


                    #region ENCOUNTERS

                    var screening = await _clientPartnerScreeningStageExtractor.Extract();
                    var pretest = screening.ToList().LastOrDefault();

                    //  PLACER_DETAIL
                    var pd = PLACER_DETAIL.Create(1, pretest.Id);

                    //  Partner_SCREENING
                    var pr = Partner_SCREENING.Create(pretest);

                    //  Partner_TRACING
                    var allTracing = await _clientPartnerTracingStageExtractor.Extract();
                    var tr = Partner_TRACING.Create(allTracing.ToList());

                    #endregion

                    messages.Add(new PartnerClientMessage(header,
                        new List<PARTNER> { new PARTNER(pid, new PARTNER_ENCOUNTER(pd, pr, tr)) }));

                }
            }

            return messages;
        }
    }
}