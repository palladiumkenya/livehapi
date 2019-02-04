using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;
using LiveHAPI.Sync.Core.Exchange.Family;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using Serilog;

namespace LiveHAPI.Sync.Core.Loader
{
    public class FamilyClientMessageLoader : IFamilyClientMessageLoader
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IClientStageRepository _clientStageRepository;
        private readonly IClientStageRelationshipRepository _clientStageRelationshipRepository;
        private readonly IClientFamilyScreeningStageExtractor _clientFamilyScreeningStageExtractor;
        private readonly IClientFamilyTracingStageExtractor _clientFamilyTracingStageExtractor;

        public FamilyClientMessageLoader(IPracticeRepository practiceRepository,
            IClientStageRepository clientStageRepository,
            IClientStageRelationshipRepository clientStageRelationshipRepository,
            IClientFamilyScreeningStageExtractor clientFamilyScreeningStageExtractor,
            IClientFamilyTracingStageExtractor clientFamilyTracingStageExtractor)
        {
            _practiceRepository = practiceRepository;
            _clientStageRepository = clientStageRepository;
            _clientStageRelationshipRepository = clientStageRelationshipRepository;
            _clientFamilyScreeningStageExtractor = clientFamilyScreeningStageExtractor;
            _clientFamilyTracingStageExtractor = clientFamilyTracingStageExtractor;
        }

        public async Task<IEnumerable<FamilyClientMessage>> Load(Guid? htsClientId = null, params LoadAction[] actions)
        {
            var messages = new List<FamilyClientMessage>();
            if (!actions.Any())
                actions = new[] {LoadAction.All};

            //  Set Facility
            var facility = _practiceRepository.GetDefault();
            if (null == facility)
                throw new Exception($"Default Faciltity Not found");

            //      MESSAGE_HEADER

            var facilityCode = facility.Code;
            var header = MESSAGE_HEADER.Create(facilityCode);

            //      CLIENT

            var familyMembers = _clientStageRelationshipRepository.GetAll(x => !x.IsPartner);

            if (!htsClientId.IsNullOrEmpty())
                familyMembers = familyMembers.Where(x => x.SecondaryClientId == htsClientId);

            foreach (var familyMember in familyMembers)
            {
                var stagedClient = _clientStageRepository.GetQueued(familyMember.SecondaryClientId);
                
                if (null != stagedClient && !stagedClient.IsIndex)
                {

                    header.UpdateMfl(stagedClient.SiteCode);

                    #region PATIENT_IDENTIFICATION

                    var pid = PARTNER_FAMILY_PATIENT_IDENTIFICATION.Create(stagedClient, familyMember.IndexClientId,
                        familyMember.Relation);

                    #endregion

                    FAMILY_ENCOUNTER encounter = null;

                    if (!actions.Contains(LoadAction.RegistrationOnly))
                    {

                        PLACER_DETAIL placerDetail = null;
                        FAMILY_SCREENING familyScreening = null;
                        List<FAMILY_TRACING> familyTracings = new List<FAMILY_TRACING>();

                        #region ENCOUNTERS

                        var screening = await _clientFamilyScreeningStageExtractor.Extract(stagedClient.ClientId);
                        var screeningStage = screening.OrderBy(x=>x.ScreeningDate).LastOrDefault();

                        //  PLACER_DETAIL

                        if (null != screeningStage)
                        {
                            placerDetail = PLACER_DETAIL.Create(screeningStage.UserId, screeningStage.Id);

                            //  FAMILY_SCREENING

                            if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.ContactScreenig))
                            {
                                familyScreening = FAMILY_SCREENING.Create(screeningStage);
                            }
                        }

                        //  FAMILY_TRACING

                        if (actions.Contains(LoadAction.All) || actions.Contains(LoadAction.ContactTracing))
                        {
                            var allTracing = await _clientFamilyTracingStageExtractor.Extract(stagedClient.ClientId);
                            if (allTracing.Any())
                            {
                                familyTracings = FAMILY_TRACING.Create(allTracing.ToList());
                            }
                        }

                        #endregion

                        encounter = new FAMILY_ENCOUNTER(placerDetail, familyScreening, familyTracings);
                    }

                    messages.Add(new FamilyClientMessage(header,
                        new List<FAMILY> {new FAMILY(pid, encounter)}, stagedClient.ClientId));

                }
            }

            return messages;
        }

        public void Dispose()
        {
            _practiceRepository?.Dispose();
            _clientStageRepository?.Dispose();
            _clientStageRelationshipRepository?.Dispose();
            _clientFamilyScreeningStageExtractor?.Dispose();
            _clientFamilyTracingStageExtractor?.Dispose();
        }
    }
}