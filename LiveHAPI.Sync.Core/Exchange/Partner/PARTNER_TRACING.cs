using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Exchange.Family;

namespace LiveHAPI.Sync.Core.Exchange.Partner
{
    public class PARTNER_TRACING
    {
        public string TRACING_DATE { get; set; }

        public int TRACING_MODE { get; set; }

        public int TRACING_OUTCOME { get; set; }

        public int CONSENT { get; set; }

        public string BOOKING_DATE { get; set; }
        public int REASONNOTCONTACTED { get; set; }
        public string REASONNOTCONTACTEDOTHER { get; set; }

        public PARTNER_TRACING()
        {
        }

        public PARTNER_TRACING(string tracingDate, int tracingMode, int tracingOutcome, int consent, string bookingDate,int reasonnotcontacted,string reasonnotcontactedother)
        {
            TRACING_DATE = tracingDate;
            TRACING_MODE = tracingMode;
            TRACING_OUTCOME = tracingOutcome;
            CONSENT = consent;
            BOOKING_DATE = bookingDate;
            REASONNOTCONTACTED = reasonnotcontacted;
            REASONNOTCONTACTEDOTHER = reasonnotcontactedother;
        }

        public static List<PARTNER_TRACING> Create(List<ClientPartnerTracingStage> stage)
        {
            var list = new List<PARTNER_TRACING>();
            foreach (var clientTracingStage in stage)
            {
                list.Add(new PARTNER_TRACING(
                    clientTracingStage.TracingDate.ToIqDateOnly(),
                    clientTracingStage.TracingMode,
                    clientTracingStage.TracingOutcome,
                    clientTracingStage.Consent.Value,
                    clientTracingStage.BookingDate.ToIqDateOnly(),
                    clientTracingStage.ReasonNotContacted.Value,
                    clientTracingStage.ReasonNotContactedOther
                    ));
            }
            return list;
        }
    }
}
