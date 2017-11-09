using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        public ClientRelationship()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientRelationship(Guid relatedClientId, string relationshipTypeId)
        {
            RelatedClientId = relatedClientId;
            RelationshipTypeId = relationshipTypeId;
        }

        public static ClientRelationship Create(RelationshipInfo address)
        {
            return new ClientRelationship(address.RelatedClientId, address.RelationshipTypeId);
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