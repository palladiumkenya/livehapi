using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberMap:Entity<Guid>
    {
        public string Name { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string SubName { get; set; }
        public string SubField { get; set; }
        public string SubType { get; set; }
        public int Group { get; set; }
        public string Mode { get; set; }
        public Guid SubscriberSystemId { get; set; }
        public SubscriberMap()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}