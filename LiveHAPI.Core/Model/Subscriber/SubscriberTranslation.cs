using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberTranslation:Entity<Guid>
    {
        public string Ref { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
        public string SubCode { get; set; }
        public string SubDisplay { get; set; }
        public string SubRef { get; set; }
        public bool IsText { get; set; }
        public Guid SubscriberSystemId { get; set; }

        public SubscriberTranslation()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}