namespace LiveHAPI.Sync.Core.Exchange.Partner
{
    public class NEWPARTNER
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }


        public NEWPARTNER()
        {
        }

        public NEWPARTNER(PARTNER_FAMILY_PATIENT_IDENTIFICATION patientIdentification)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
        }
    }
}
