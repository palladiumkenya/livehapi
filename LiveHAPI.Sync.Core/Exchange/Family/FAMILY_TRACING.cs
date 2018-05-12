using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Exchange.Encounters;

namespace LiveHAPI.Sync.Core.Exchange.Family
{
    public class FAMILY_TRACING
    {
        public string TRACING_DATE { get; set; }
        public string REMINDER_DATE { get; set; }
        public int TRACING_MODE { get; set; }
        public int TRACING_OUTCOME { get; set; }
        public int CONSENT { get; set; }
        public string BOOKING_DATE { get; set; }

        public FAMILY_TRACING()
        {
        }

        private FAMILY_TRACING(string tracingDate, string reminderDate, int tracingMode, int tracingOutcome, int consent, string bookingDate)
        {
            TRACING_DATE = tracingDate;
            REMINDER_DATE = reminderDate;
            TRACING_MODE = tracingMode;
            TRACING_OUTCOME = tracingOutcome;
            CONSENT = consent;
            BOOKING_DATE = bookingDate;
        }

        public static List<FAMILY_TRACING> Create(List<ClientFamilyTracingStage> stage)
        {
            var list = new List<FAMILY_TRACING>();
            foreach (var clientTracingStage in stage)
            {
                list.Add(new FAMILY_TRACING(
                    clientTracingStage.TracingDate.ToIqDateOnly(), 
                    clientTracingStage.ReminderDate.ToIqDateOnly(),
                    clientTracingStage.TracingMode, 
                    clientTracingStage.TracingOutcome,
                    clientTracingStage.Consent.Value,
                    clientTracingStage.BookingDate.ToIqDateOnly()));
            }
            return list;
        }
    }
}