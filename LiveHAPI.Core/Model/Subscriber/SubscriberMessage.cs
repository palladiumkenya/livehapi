using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberMessage:Entity<Guid>
    {
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public decimal Rank { get; set; }
        public string Description { get; set; }
        public bool Processed { get; set; }
        public DateTime DateProcessed { get; set; }
        public Guid SubscriberSystemId { get; set; }

        public SubscriberMessage()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}