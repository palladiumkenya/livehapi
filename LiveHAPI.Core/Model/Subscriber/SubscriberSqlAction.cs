using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberSqlAction:Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        public string Action { get; set; }

        public SubscriberSqlAction()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}
