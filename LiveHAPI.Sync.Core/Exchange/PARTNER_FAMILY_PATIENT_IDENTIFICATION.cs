using System;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Exchange.Clients;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class PARTNER_FAMILY_PATIENT_IDENTIFICATION : PATIENT_IDENTIFICATION
    {
        public int RELATIONSHIP_TYPE { get; set; }

        public static PARTNER_FAMILY_PATIENT_IDENTIFICATION Create(ClientStage stagedClient, Guid indexClientId, int relation)
        {
            var pid = CreateContact(stagedClient, indexClientId);
            var cid = pid as PARTNER_FAMILY_PATIENT_IDENTIFICATION;
            cid.RELATIONSHIP_TYPE = relation;
            return cid;
        }
    }
}