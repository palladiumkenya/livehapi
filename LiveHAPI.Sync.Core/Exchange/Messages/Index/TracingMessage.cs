using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Index
{
    public class TracingMessage : ClientMessage
    {
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; } = new List<INTERNAL_PATIENT_ID>();
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public NewReferral REFERRAL { get; set; }
        public List<NewTracing> TRACING { get; set; }
        
        public TracingMessage()
        {
        }

        private TracingMessage(MESSAGE_HEADER messageHeader, Guid clientId,List<INTERNAL_PATIENT_ID> patientIds, PLACER_DETAIL placerDetail, List<NewTracing> tracings) : base(messageHeader, clientId)
        {
            INTERNAL_PATIENT_ID = patientIds;
            PLACER_DETAIL = placerDetail;
            TRACING = tracings;
        }
        
        public static TracingMessage Create(IndexClientMessage indexClientMessage)
        {
            if(null==indexClientMessage)
                throw new ArgumentException("message cannot be null");
            
            indexClientMessage.ValidateTracing();
            
            return new TracingMessage(
                indexClientMessage.MESSAGE_HEADER,
                indexClientMessage.ClientId,
                indexClientMessage.CurrentClient().PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID,
                indexClientMessage.CurrentClient().ENCOUNTER.PLACER_DETAIL,
                indexClientMessage.CurrentClient().ENCOUNTER.TRACING);
        }
    }
}