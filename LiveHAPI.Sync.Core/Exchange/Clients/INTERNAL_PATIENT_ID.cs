using System;
using System.Collections.Generic;

namespace LiveHAPI.Sync.Core.Exchange.Clients
{
    public class INTERNAL_PATIENT_ID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }

        public INTERNAL_PATIENT_ID()
        {
        }

        private INTERNAL_PATIENT_ID(string id, string identifierType, string assigningAuthority)
        {
            ID = id;
            IDENTIFIER_TYPE = identifierType;
            ASSIGNING_AUTHORITY = assigningAuthority;
        }

        public static INTERNAL_PATIENT_ID CreateSerial(string serial)
        {
            return new INTERNAL_PATIENT_ID(serial, "HTS_SERIAL", "HTS");
        }

        public static INTERNAL_PATIENT_ID CreateAfyaId(Guid clientId)
        {
            return new INTERNAL_PATIENT_ID(clientId.ToString(), "AFYA_MOBILE_ID", "AFYAMOBILE");
        }

        public static INTERNAL_PATIENT_ID CreateContactAfyaId(Guid indexClientId)
        {
            return new INTERNAL_PATIENT_ID(indexClientId.ToString(), "INDEX_CLIENT_AFYAMOBILE_ID", "AFYAMOBILE");
        }
        public static List<INTERNAL_PATIENT_ID> Create(Guid clientId, string serial)
        {
            return
                new List<INTERNAL_PATIENT_ID>
                {
                    CreateAfyaId(clientId),
                    CreateSerial(serial)
                };
        }
        public static List<INTERNAL_PATIENT_ID> CreateContact(Guid clientId, Guid indexClientId)
        {
            return
                new List<INTERNAL_PATIENT_ID>
                {
                    CreateAfyaId(clientId),
                    CreateContactAfyaId(indexClientId)
                };
        }
    }
}