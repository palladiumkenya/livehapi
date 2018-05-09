using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Encounters;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class ENCOUNTERS
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PRE_TEST PRE_TEST { get; set; }
        public HIV_TESTS HIV_TESTS { get; set; }
        public NewReferral REFERRAL { get; set; }
        public List<NewTracing> TRACING { get; set; }
        public NewLinkage LINKAGE { get; set; }

        public ENCOUNTERS()
        {
        }

        private ENCOUNTERS(PLACER_DETAIL placerDetail, PRE_TEST preTest, HIV_TESTS hivTests, NewReferral referral, List<NewTracing> tracing, NewLinkage linkage)
        {
            PLACER_DETAIL = placerDetail;
            PRE_TEST = preTest;
            HIV_TESTS = hivTests;
            REFERRAL = referral;
            TRACING = tracing;
            LINKAGE = linkage;
        }

        public static ENCOUNTERS Create(PLACER_DETAIL placerDetail, PRE_TEST preTest, HIV_TESTS hivTests, NewReferral referral, List<NewTracing> tracing, NewLinkage linkage)
        {
            return new ENCOUNTERS(placerDetail,preTest,hivTests,referral,tracing,linkage);
        }
    }
}