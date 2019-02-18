using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Sync.Core.Exchange.Clients;
using LiveHAPI.Sync.Core.Exchange.Encounters;

namespace LiveHAPI.Sync.Core.Exchange.Messages.Index
{
    public class PretestMessage : ClientMessage
    {
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; } = new List<INTERNAL_PATIENT_ID>();
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PRE_TEST PRE_TEST { get; set; }

        public PretestMessage()
        {
        }

        private PretestMessage(MESSAGE_HEADER messageHeader, Guid clientId,List<INTERNAL_PATIENT_ID> patientIds, PLACER_DETAIL placerDetail, PRE_TEST preTest) : base(messageHeader, clientId)
        {
            INTERNAL_PATIENT_ID = patientIds;
            PLACER_DETAIL = placerDetail;
            PRE_TEST = preTest;
        }
        
        public static PretestMessage Create(IndexClientMessage indexClientMessage)
        {
            if(null==indexClientMessage)
                throw new ArgumentException("message cannot be null");
            
            indexClientMessage.ValidatePretest();
            
            
            return new PretestMessage(
                indexClientMessage.MESSAGE_HEADER,
                indexClientMessage.ClientId,
                indexClientMessage.CurrentClient().PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID,
                indexClientMessage.CurrentClient().ENCOUNTER.PLACER_DETAIL,
                indexClientMessage.CurrentClient().ENCOUNTER.PRE_TEST);
        }
        
    }
}