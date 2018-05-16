using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Subscriber
{
    public class SubscriberSystem : Entity<Guid>
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<SubscriberConfig> Configs { get; set; } = new List<SubscriberConfig>();
        public ICollection<SubscriberSqlAction> Actions { get; set; } = new List<SubscriberSqlAction>();
        public ICollection<SubscriberMessage> Messages { get; set; } = new List<SubscriberMessage>();
        public ICollection<SubscriberMap> Maps { get; set; } = new List<SubscriberMap>();
        public ICollection<SubscriberTranslation> Translations { get; set; } = new List<SubscriberTranslation>();
        public ICollection<SubscriberCohort> Cohorts { get; set; } = new List<SubscriberCohort>();

        [NotMapped] public List<User> Users { get; set; } = new List<User>();

        public SubscriberSystem()
        {
            Id = LiveGuid.NewGuid();
        }

        public string GetTranslation(object code, string subref, string def)
        {
            if (null == code)
                return def;
            
            var translation =
                Translations.FirstOrDefault(x => x.SubRef.IsSameAs(subref) &&
                                                 x.Code.IsSameAs(code.ToString()));

            if (null != translation)
                return translation.SubCode;

            return def;
        }

        public string GetTranslation(object code, string subref, string hapiRef, string def)
        {
            if (null == code)
                return def;
            
            var translation =
                Translations.FirstOrDefault(x => x.Ref.IsSameAs(hapiRef) &&
                                                 x.SubRef.IsSameAs(subref) &&
                                                 x.Code.IsSameAs(code.ToString()));

            if (null != translation)
                return translation.SubCode;

            return def;
        }
        
        
        
        public int GetEmrUserId(Guid userId)
        {
            var user = Users.FirstOrDefault(x => x.Id == userId);
            if (null != user)
            {
                int.TryParse(user.SourceRef, out var emrUserId);
                return emrUserId;
            }

            return 1;
        }
    }
}
