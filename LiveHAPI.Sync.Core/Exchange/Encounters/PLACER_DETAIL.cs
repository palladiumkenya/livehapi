using System;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class PLACER_DETAIL
    {
        public int PROVIDER_ID { get; set; }
        public string ENCOUNTER_NUMBER { get; set; }

        public PLACER_DETAIL()
        {
        }

        private PLACER_DETAIL(int providerId, string encounterNumber)
        {
            PROVIDER_ID = providerId;
            ENCOUNTER_NUMBER = encounterNumber;
        }

        public static PLACER_DETAIL Create(int providerId, Guid encounterNumber)
        {
            return new PLACER_DETAIL(providerId,encounterNumber.ToString());
        }
    }
}