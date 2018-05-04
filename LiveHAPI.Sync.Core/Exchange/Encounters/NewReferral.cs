using System;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class NewReferral
    {
        public string REFERRED_TO { get; set; }
        public string DATE_TO_BE_ENROLLED { get; set; }

        public NewReferral()
        {
        }

        private NewReferral(string referredTo, string dateToBeEnrolled)
        {
            REFERRED_TO = referredTo;
            DATE_TO_BE_ENROLLED = dateToBeEnrolled;
        }

        public static NewReferral Create(string referredTo, DateTime dateToBeEnrolled)
        {
           return new NewReferral(referredTo,dateToBeEnrolled.ToIqDateOnly());
        }
    }
}