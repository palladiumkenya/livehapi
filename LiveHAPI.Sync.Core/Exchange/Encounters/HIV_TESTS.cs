using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Enum;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class HIV_TESTS
    {
        public List<NewTests> SCREENING { get; set; }=new List<NewTests>();
        public List<NewTests> CONFIRMATORY { get; set; } =new List<NewTests>();
        public SUMMARY SUMMARY { get; set; }

        public HIV_TESTS()
        {
        }

        private HIV_TESTS(List<NewTests> screening, List<NewTests> confirmatory, SUMMARY summary)
        {
            SCREENING = screening;
            CONFIRMATORY = confirmatory;
            SUMMARY = summary;
        }

        public static HIV_TESTS Create(List<NewTests> screening, List<NewTests> confirmatory, SUMMARY summary)
        {
            return new HIV_TESTS(screening,confirmatory,summary);
        }

        public static HIV_TESTS Create(List<ClientTestingStage> tests, SUMMARY summary)
        {
            var screening = NewTests.Create(tests.Where(x => x.HtsTestType == HtsTestType.Screening));
            var confirmatory = NewTests.Create(tests.Where(x => x.HtsTestType == HtsTestType.Confrimatory));
            return new HIV_TESTS(screening, confirmatory, summary);
        }
    }
}