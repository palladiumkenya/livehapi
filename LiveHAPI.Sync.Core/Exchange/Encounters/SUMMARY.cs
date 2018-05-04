namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class SUMMARY
    {
        public int SCREENING_RESULT { get; set; }
        public int CONFIRMATORY_RESULT { get; set; }
        public int FINAL_RESULT { get; set; }
        public int FINAL_RESULT_GIVEN { get; set; }
        public int COUPLE_DISCORDANT { get; set; }
        public int PNS_ACCEPTED { get; set; }
        public int PNS_DECLINE_REASON { get; set; }
        public string REMARKS { get; set; }

        public SUMMARY()
        {
        }

        private SUMMARY(int screeningResult, int confirmatoryResult, int finalResult, int finalResultGiven, int coupleDiscordant, int pnsAccepted, int pnsDeclineReason, string remarks)
        {
            SCREENING_RESULT = screeningResult;
            CONFIRMATORY_RESULT = confirmatoryResult;
            FINAL_RESULT = finalResult;
            FINAL_RESULT_GIVEN = finalResultGiven;
            COUPLE_DISCORDANT = coupleDiscordant;
            PNS_ACCEPTED = pnsAccepted;
            PNS_DECLINE_REASON = pnsDeclineReason;
            REMARKS = remarks;
        }

        public static SUMMARY Create(int screeningResult, int confirmatoryResult, int finalResult, int finalResultGiven, int coupleDiscordant, int pnsAccepted, int pnsDeclineReason, string remarks)
        {
            return new SUMMARY(screeningResult,confirmatoryResult,finalResult,finalResultGiven,coupleDiscordant,pnsAccepted,pnsDeclineReason,remarks);
        }
    }
}