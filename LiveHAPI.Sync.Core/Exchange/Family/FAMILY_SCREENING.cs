using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Family
{
    public class FAMILY_SCREENING
    {
        public string SCREENING_DATE { get; set; }
        public int HIV_STATUS { get; set; }
        public int ELIGIBLE_FOR_HTS { get; set; }
        public string BOOKING_DATE { get; set; }
        public string REMARKS { get; set; }

        public FAMILY_SCREENING()
        {
        }

        private FAMILY_SCREENING(string screeningDate, int hivStatus, int eligibleForHts, string bookingDate, string remarks)
        {
            SCREENING_DATE = screeningDate;
            HIV_STATUS = hivStatus;
            ELIGIBLE_FOR_HTS = eligibleForHts;
            BOOKING_DATE = bookingDate;
            REMARKS = remarks;
        }

        public static FAMILY_SCREENING Create(ClientFamilyScreeningStage clientTracingStage)
        {
            return new FAMILY_SCREENING(
                clientTracingStage.ScreeningDate.ToIqDateOnly(),
                clientTracingStage.HivStatus,
                clientTracingStage.EligibleForHts,
                clientTracingStage.BookingDate.ToIqDateOnly(),
                clientTracingStage.Remarks);
        }
       
        public static List<FAMILY_SCREENING> Create(List<ClientFamilyScreeningStage> stage)
        {
            var list = new List<FAMILY_SCREENING>();
            foreach (var clientTracingStage in stage)
            {
                list.Add(Create(clientTracingStage));
            }
            return list;
        }
    }
}