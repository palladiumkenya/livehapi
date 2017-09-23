using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberConfig:Entity<Guid>
    {
        public string Area { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid SubscriberSystemId { get; set; }

        public SubscriberConfig()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}