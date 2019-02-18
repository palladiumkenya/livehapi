using System.Collections.Generic;

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
        
        public static NEWPARTNER Create(PARTNER partner)
        {
            return new NEWPARTNER(partner.PATIENT_IDENTIFICATION);
        }

        public static List<NEWPARTNER> Create(List<PARTNER> newPartners)
        {
            var newpartners=new List<NEWPARTNER>();

            foreach (var partner in newPartners)
            {
                newpartners.Add(Create(partner));
            }

            return newpartners;
        }
    }
}
