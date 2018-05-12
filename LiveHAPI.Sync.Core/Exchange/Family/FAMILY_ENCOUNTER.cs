using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Encounters;

namespace LiveHAPI.Sync.Core.Exchange.Family
{
    public class FAMILY_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public FAMILY_SCREENING FAMILY_SCREENING { get; set; }
        public List<FAMILY_TRACING> TRACING { get; set; }

        public FAMILY_ENCOUNTER()
        {
        }

        public FAMILY_ENCOUNTER(PLACER_DETAIL placerDetail, FAMILY_SCREENING familyScreening, List<FAMILY_TRACING> tracing)
        {
            PLACER_DETAIL = placerDetail;
            FAMILY_SCREENING = familyScreening;
            TRACING = tracing;
        }
    }
}