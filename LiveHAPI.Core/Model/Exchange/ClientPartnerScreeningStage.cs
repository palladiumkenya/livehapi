using System;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientPartnerScreeningStage : Entity<Guid>
    {
        public Guid IndexClientId { get; set; }
        public int Relation { get; set; }
        public bool IsPartner { get; set; }
        public Guid SecondaryClientId { get; set; }
        public SyncStatus SyncStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public string SyncStatusInfo { get; set; }

        public ClientPartnerScreeningStage()
        {
            Id = LiveGuid.NewGuid();
            SyncStatus = SyncStatus.Staged;
            StatusDate = DateTime.Now;
        }

        private ClientPartnerScreeningStage(Guid indexClientId, int relation, Guid secondaryClientId, bool isPartner) :
            this()
        {
            IndexClientId = indexClientId;
            Relation = relation;
            SecondaryClientId = secondaryClientId;
            IsPartner = isPartner;
        }

        public static ClientPartnerScreeningStage Create(ClientRelationship relationship, SubscriberSystem subscriber)
        {
            var clientStage = new ClientPartnerScreeningStage();

            if (null != relationship)
            {
                clientStage.IndexClientId = relationship.ClientId;
                clientStage.IsPartner = relationship.IsPartner;
                clientStage.SecondaryClientId = relationship.RelatedClientId;
                clientStage.Relation = subscriber.GetTranslation(relationship.RelationshipTypeId, "Relationship", "0").SafeConvert<int>(); ;
            }

            return clientStage;
        }

        public override string ToString()
        {
            return $"{IndexClientId} {Relation}  {SecondaryClientId} [{IsPartner}] ";
        }
    }
}