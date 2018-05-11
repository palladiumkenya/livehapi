using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class ClientRelationship : Entity<Guid>, IClientRelationship
    {
        public Guid RelatedClientId { get; set; }
        [MaxLength(50)]
        public string RelationshipTypeId { get; set; }
        public bool Preferred { get; set; }
        public Guid ClientId { get; set; }
        public bool? IsIndex { get; set; }

        [NotMapped]
        public bool IsPartner
        {
            get { return RelationshipTypeId.IsSameAs("Cowife") || RelationshipTypeId.IsSameAs("Partner") || RelationshipTypeId.IsSameAs("Spouse"); }
        }

        [NotMapped]
        public bool ClientIsIndex
        {
            get { return null != IsIndex && !IsIndex.Value; }
        }

        public ClientRelationship()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientRelationship(Guid id, Guid relatedClientId, string relationshipTypeId, bool? isIndex):base(id)
        {
            RelatedClientId = relatedClientId;
            RelationshipTypeId = relationshipTypeId;
            IsIndex = isIndex;
        }

        public static ClientRelationship Create(RelationshipInfo address)
        {
            return new ClientRelationship(address.Id, address.RelatedClientId, address.RelationshipTypeId,address.IsIndex);
        }

        public static List<ClientRelationship> Create(ClientInfo clientInfo)
        {
            var list = new List<ClientRelationship>();

            foreach (var address in clientInfo.Relationships)
            {
                list.Add(Create(address));
            }
            return list;
        }

        public static List<RelationshipInfo> GetClientRelationshipInfos(List<ClientRelationship> clientRelationships)
        {
            var list = new List<RelationshipInfo>();
            foreach (var clientClientRelationship in clientRelationships)
            {
                list.Add(clientClientRelationship.GetClientRelationshipInfo());
            }

            return list;
        }

        public RelationshipInfo GetClientRelationshipInfo()
        {
            return new RelationshipInfo(RelatedClientId, RelationshipTypeId,ClientId);
        }
    }
}