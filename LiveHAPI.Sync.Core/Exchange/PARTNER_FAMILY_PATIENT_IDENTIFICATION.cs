using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Exchange.Clients;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class PARTNER_FAMILY_PATIENT_IDENTIFICATION : PATIENT_IDENTIFICATION
    {
        public int RELATIONSHIP_TYPE { get; set; }

        protected PARTNER_FAMILY_PATIENT_IDENTIFICATION(ClientStage clientStage, Guid indexClientId, int relationshipType) : base(clientStage, indexClientId)
        {
            RELATIONSHIP_TYPE = relationshipType;
        }

        public static PARTNER_FAMILY_PATIENT_IDENTIFICATION Create(ClientStage stagedClient, Guid indexClientId, int relation)
        {
            return new PARTNER_FAMILY_PATIENT_IDENTIFICATION(stagedClient,indexClientId,relation);
        }
    }
}