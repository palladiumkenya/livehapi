using LiveHAPI.Sync.Core.Exchange.Partner;

namespace LiveHAPI.Sync.Core.Exchange.Family
{
    public class FAMILY
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public FAMILY_ENCOUNTER ENCOUNTER { get; set; }

        public FAMILY()
        {
        }

        public FAMILY(PARTNER_FAMILY_PATIENT_IDENTIFICATION patientIdentification, FAMILY_ENCOUNTER encounter)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
            ENCOUNTER = encounter;
        }
    }
}