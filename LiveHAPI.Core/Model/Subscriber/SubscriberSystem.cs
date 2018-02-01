using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberSystem : Entity<Guid>
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<SubscriberConfig> Configs { get; set; }
        public ICollection<SubscriberSqlAction> Actions { get; set; }
        public ICollection<SubscriberMessage> Messages { get; set; }
        public ICollection<SubscriberMap> Maps { get; set; }
        public ICollection<SubscriberTranslation> Translations { get; set; }
        public ICollection<SubscriberCohort> Cohorts { get; set; }

        [NotMapped]
        public List<User> Users { get; set; }

        public SubscriberSystem()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}
