using System;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class MESSAGE_HEADER
    {
        public string SENDING_APPLICATION { get; set; }
        public string SENDING_FACILITY { get; set; }
        public string RECEIVING_APPLICATION { get; set; }
        public string RECEIVING_FACILITY { get; set; }
        public string MESSAGE_DATETIME { get; set; }
        public string SECURITY { get; set; }
        public string MESSAGE_TYPE { get; set; }
        public string PROCESSING_ID { get; set; }

        public MESSAGE_HEADER()
        {
        }

        private MESSAGE_HEADER(string sendingApplication, string sendingFacility, string receivingApplication, string receivingFacility, string messageDatetime, string security, string messageType, string processingId)
        {
            SENDING_APPLICATION = sendingApplication;
            SENDING_FACILITY = sendingFacility;
            RECEIVING_APPLICATION = receivingApplication;
            RECEIVING_FACILITY = receivingFacility;
            MESSAGE_DATETIME = messageDatetime;
            SECURITY = security;
            MESSAGE_TYPE = messageType;
            PROCESSING_ID = processingId;
        }

        public static MESSAGE_HEADER Create()
        {
            return Create(string.Empty,null);
        }

        public static MESSAGE_HEADER Create(string sendingFacility, DateTime? messageDatetime)
        {
            return new MESSAGE_HEADER(
                "HAPI",
                sendingFacility,
                "IQCARE",
                sendingFacility,
                messageDatetime.ToIqDate(),
                "",
                "HTS^A04",
                "P");
        }
    }
}