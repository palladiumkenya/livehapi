using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Shared.Enum;
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
        private readonly IClientStageRelationshipRepository _clientStageRelationshipRepository;
        private readonly IClientPartnerScreeningStageExtractor _clientPartnerScreeningStageExtractor;
        private readonly IClientPartnerTracingStageExtractor _clientPartnerTracingStageExtractor;

        public PartnerClientMessageLoader(IPracticeRepository practiceRepository,
            IClientStageRepository clientStageRepository,
            IClientStageRelationshipRepository clientStageRelationshipRepository,
            IClientPartnerScreeningStageExtractor clientPartnerScreeningStageExtractor,
            IClientPartnerTracingStageExtractor clientPartnerTracingStageExtractor)
        {
            _practiceRepository = practiceRepository;
            _clientStageRepository = clientStageRepository;
            _clientStageRelationshipRepository = clientStageRelationshipRepository;
            _clientPartnerScreeningStageExtractor = clientPartnerScreeningStageExtractor;
            _clientPartnerTracingStageExtractor = clientPartnerTracingStageExtractor;
        }

        public async Task<IEnumerable<PartnerClientMessage>> Load(Guid? htsClientId, params LoadAction[] actions)
        {
            var messages = new List<PartnerClientMessage>();
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

            var fams = _clientStageRelationshipRepository.GetAll(x => x.IsPartner);

            foreach (var fam in fams)
            {
                var stagedClient = _clientStageRepository.Get(fam.SecondaryClientId);
                if (null != stagedClient && !stagedClient.IsIndex)
                {
                    #region PATIENT_IDENTIFICATION

                    var pid = PARTNER_FAMILY_PATIENT_IDENTIFICATION.Create(stagedClient, fam.IndexClientId,fam.Relation);

                    #endregion

                    PARTNER_ENCOUNTER encounter = null;
                  
                    if (!actions.Contains(LoadAction.RegistrationOnly))
                    {
                        PLACER_DETAIL placerDetail = null;
                        PARTNER_SCREENING partnerScreening = null;
                        List<PARTNER_TRACING> partnerTracings=new List<PARTNER_TRACING>();
                        
                        #region ENCOUNTERS

                        var screening = await _clientPartnerScreeningStageExtractor.Extract(stagedClient.ClientId);
                        var screeningStage = screening.OrderBy(x=>x.ScreeningDate).LastOrDefault();

                        //  PLACER_DETAIL

                        if (null != screeningStage)
                        {
                            placerDetail = PLACER_DETAIL.Create(screeningStage.UserId, screeningStage.Id);

                            //  PARTNER_SCREENING
                            if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.ContactScreenig))
                            {
                                partnerScreening = PARTNER_SCREENING.Create(screeningStage);
                            }
                        }

                        //  Partner_TRACING
                        if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.ContactTracing))
                        {
                            var allTracing = await _clientPartnerTracingStageExtractor.Extract(stagedClient.ClientId);
                            if (allTracing.Any())
                            {
                                partnerTracings = PARTNER_TRACING.Create(allTracing.ToList());
                            }
                        }

                        #endregion
                        
                        encounter = new PARTNER_ENCOUNTER(placerDetail, partnerScreening,partnerTracings);
                    }

                    messages.Add(new PartnerClientMessage(header,
                        new List<PARTNER> { new PARTNER(pid,encounter) }));

                }
            }

            return messages;
        }
    }
}