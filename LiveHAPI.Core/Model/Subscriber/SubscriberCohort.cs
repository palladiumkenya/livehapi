using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberCohort : Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public int Rank { get; set; }
        public string View { get; set; }
        public Guid SubscriberSystemId { get; set; }

        public SubscriberCohort()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}
