using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Sync.Core.Exchange.Family;
using LiveHAPI.Sync.Core.Exchange.Messages.Familiy;

namespace LiveHAPI.Sync.Core.Exchange.Messages
{
    public class FamilyClientMessage:ClientMessage
    {
        public List<FAMILY> FAMILY { get; set; }
        public FAMILY CurrentClient() => FAMILY.FirstOrDefault();

        public FamilyClientMessage()
        {
        }

        public FamilyClientMessage(MESSAGE_HEADER messageHeader, List<FAMILY> family, Guid clientId)
        {
            MESSAGE_HEADER = messageHeader;
            FAMILY = family;
            ClientId = clientId;
        }

        public FamilyMessage GetDemographicMessage()
        {
            return FamilyMessage.Create(this);
        }

        public FamilyScreening GetScreeningMessage()
        {
            return FamilyScreening.Create(this);
        }

        public FamilyTracing GetTracingMessage()
        {
            return FamilyTracing.Create(this);
        }
        public void ValidateDemographics()
        {
            if (!FAMILY.Any())
                throw new ArgumentException("no clients found");

            var family = FAMILY.First();

            if (null == family.PATIENT_IDENTIFICATION)
                throw new ArgumentException("missing patient Identification");

            if (!family.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Any())
                throw new ArgumentException("client has no identification");
        }

        public void ValidateEncounter()
        {
            ValidateDemographics();

            if(null==CurrentClient().ENCOUNTER)
                throw new ArgumentException("missing Encounter");

            if(null==CurrentClient().ENCOUNTER.PLACER_DETAIL)
                throw new ArgumentException("missing Encounter user");
        }

        public void ValidateFamilyScreening()
        {
            ValidateEncounter();

            if (null == CurrentClient().ENCOUNTER.FAMILY_SCREENING)
                throw new ArgumentException("missing screening Encounter");
        }

        public void ValidateTracing()
        {
            ValidateEncounter();

            if (!CurrentClient().ENCOUNTER.TRACING.Any())
                throw new ArgumentException("missing Tracing Encounter");
        }
    }
}
