using System.Collections.Generic;

namespace LiveHAPI.Sync.Core.Exchange.Family
{
    public class NEWFAMILY
    {
        public PARTNER_FAMILY_PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }

        public NEWFAMILY()
        {
        }

        public NEWFAMILY(PARTNER_FAMILY_PATIENT_IDENTIFICATION patientIdentification)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
        }

        public static NEWFAMILY Create(FAMILY newFamily)
        {
            return new NEWFAMILY(newFamily.PATIENT_IDENTIFICATION);
        }

        public static List<NEWFAMILY> Create(List<FAMILY> newFamilys)
        {
            var newfamilys=new List<NEWFAMILY>();

            foreach (var newFamily in newFamilys)
            {
                newfamilys.Add(Create(newFamily));
            }

            return newfamilys;
        }
    }
}
