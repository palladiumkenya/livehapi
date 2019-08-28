using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class NewTracing
    {
        public string TRACING_DATE { get; set; }
        public int TRACING_MODE { get; set; }
        public int TRACING_OUTCOME { get; set; }
        public int REASONNOTCONTACTED { get; set; }
        public string REASONNOTCONTACTEDOTHER { get; set; }

        public NewTracing()
        {
        }

        private NewTracing(string tracingDate, int tracingMode, int tracingOutcome,int reasonnotcontacted,string reasonnotcontactedother)
        {
            TRACING_DATE = tracingDate;
            TRACING_MODE = tracingMode;
            TRACING_OUTCOME = tracingOutcome;
            REASONNOTCONTACTED = reasonnotcontacted;
            REASONNOTCONTACTEDOTHER = reasonnotcontactedother;
        }

        public static NewTracing Create(DateTime tracingDate, int tracingMode, int tracingOutcome,int reasonnotcontacted,string reasonnotcontactedother)
        {
            return new NewTracing(tracingDate.ToIqDateOnly(),tracingMode,tracingOutcome,reasonnotcontacted,reasonnotcontactedother);
        }

        public static List<NewTracing> Create(List<ClientTracingStage> stage)
        {
            var list = new List<NewTracing>();
            foreach (var clientTracingStage in stage)
            {
                  list.Add(new NewTracing(clientTracingStage.TracingDate.ToIqDateOnly(),clientTracingStage.TracingMode,clientTracingStage.TracingOutcome,clientTracingStage.ReasonNotContacted.Value,clientTracingStage.ReasonNotContactedOther));
            }
            return list;
        }
    }
}
