using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Encounters;

namespace LiveHAPI.Sync.Core.Exchange.Partner
{
    public class PARTNER_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PARTNER_SCREENING PARTNER_SCREENING { get; set; }
        public List<PARTNER_TRACING> TRACING { get; set; }

        public PARTNER_ENCOUNTER()
        {
        }

        public PARTNER_ENCOUNTER(PLACER_DETAIL placerDetail, PARTNER_SCREENING partnerScreening, List<PARTNER_TRACING> tracing)
        {
            PLACER_DETAIL = placerDetail;
            PARTNER_SCREENING = partnerScreening;
            TRACING = tracing;
        }
    }
}