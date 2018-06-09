using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class NewTests
    {
        public int KIT_TYPE { get; set; }
        public string KIT_OTHER { get; set; }
        public string LOT_NUMBER { get; set; }
        public string EXPIRY_DATE { get; set; }
        public int RESULT { get; set; }
        public int TEST_ROUND { get; set; }

        public NewTests()
        {
        }

        private NewTests(int kitType, string kitOther, string lotNumber, string expiryDate, int result, int testRound)
        {
            KIT_TYPE = kitType;
            KIT_OTHER = kitOther;
            LOT_NUMBER = lotNumber;
            EXPIRY_DATE = expiryDate;
            RESULT = result;
            TEST_ROUND = testRound;
        }

        public static NewTests Create(int kitType, string kitOther, string lotNumber, DateTime expiryDate, int result,
            int testRound)
        {
            return new NewTests(kitType, kitOther, lotNumber, expiryDate.ToIqDateOnly(), result, testRound);
        }

        internal static List<NewTests> Create(IEnumerable<ClientTestingStage> stages)
        {
            var list = new List<NewTests>();

            foreach (var stage in stages)
            {
                list.Add(
                    new NewTests(
                        stage.KitType, stage.KitOther, stage.LotNumber, stage.ExpiryDate, stage.Result, stage.TestRound
                    ));
            }

            return list;
        }
    }
}