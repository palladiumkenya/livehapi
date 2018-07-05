using System;
using LiveHAPI.Shared;
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
        /// <summary>
        /// IQCare ItemName
        /// </summary>
        public string SubDisplay { get; set; }
        /// <summary>
        /// IQCare MasterName
        /// </summary>
        public string SubRef { get; set; }
        public bool IsText { get; set; }
        public int Group { get; set; }
        public Guid SubscriberSystemId { get; set; }

        public SubscriberTranslation()
        {
            Id = LiveGuid.NewGuid();
        }

        public bool IsUpdated()
        {
            return Id == new Guid("ab88ad8c-88b8-11e7-abc4-cec278b6b50a") && Code.IsSameAs(Defualts.SyncVersion);
        }

        public bool HasSub()
        {
            return !string.IsNullOrWhiteSpace(SubRef);
        }

        public override string ToString()
        {
            return $"{Ref}|{Display}|{Code} >><< {SubCode}|{SubDisplay} [{SubRef}]";

        }

        public void UpdateTo(SubscriberTranslation practice)
        {
            SubCode = practice.SubCode;
        }
    }
}