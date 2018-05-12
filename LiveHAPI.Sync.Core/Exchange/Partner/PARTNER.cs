namespace LiveHAPI.Sync.Core.Exchange.Partner
{
    public class PARTNER
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public PARTNER_ENCOUNTER ENCOUNTER { get; set; }

        public PARTNER()
        {
        }

        public PARTNER(PARTNER_FAMILY_PATIENT_IDENTIFICATION patientIdentification, PARTNER_ENCOUNTER encounter)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
            ENCOUNTER = encounter;
        }
    }
}