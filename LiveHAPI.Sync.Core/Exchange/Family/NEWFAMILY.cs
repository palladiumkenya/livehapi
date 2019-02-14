namespace LiveHAPI.Sync.Core.Exchange.Family
{
    public class NEWFAMILY
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public FAMILY_ENCOUNTER ENCOUNTER { get; set; }

        public NEWFAMILY()
        {
        }

        public NEWFAMILY(PARTNER_FAMILY_PATIENT_IDENTIFICATION patientIdentification, FAMILY_ENCOUNTER encounter)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
            ENCOUNTER = encounter;
        }
    }
}