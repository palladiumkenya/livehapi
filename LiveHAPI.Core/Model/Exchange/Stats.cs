namespace LiveHAPI.Core.Model.Exchange
{
    public class Stats
    {
        public int Received { get; set; }
        public int Sent { get; set; }
        public int Failed => Received - Sent;

        public Stats(int received, int sent)
        {
            Received = received;
            Sent = sent;
        }
    }
}