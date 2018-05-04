using System;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class NewTracing
    {
        public string TRACING_DATE { get; set; }
        public int TRACING_MODE { get; set; }
        public int TRACING_OUTCOME { get; set; }

        public NewTracing()
        {
        }

        private NewTracing(string tracingDate, int tracingMode, int tracingOutcome)
        {
            TRACING_DATE = tracingDate;
            TRACING_MODE = tracingMode;
            TRACING_OUTCOME = tracingOutcome;
        }

        public static NewTracing Create(DateTime tracingDate, int tracingMode, int tracingOutcome)
        {
            return new NewTracing(tracingDate.ToIqDateOnly(),tracingMode,tracingOutcome);
        }
    }
}