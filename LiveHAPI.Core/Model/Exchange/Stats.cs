using System;

namespace LiveHAPI.Core.Model.Exchange
{
    public class Stats
    {
        public int Received { get; set; }
        public int Staged { get; set; }
        public int Sent { get; set; }
        public int Failed { get; set; }
        public Guid ProviderId { get; set; }

        public Stats(int received, int staged, int sent, int failed)
        {
            Received = received;
            Staged = staged;
            Sent = sent;
            Failed = failed;
        }

        public void SetProvider(Guid providerId)
        {
            ProviderId = providerId;
        }
    }
}
