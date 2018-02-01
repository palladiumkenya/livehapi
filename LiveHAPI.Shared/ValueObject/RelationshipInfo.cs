using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class RelationshipInfo : IClientRelationship
    {
        public Guid RelatedClientId { get; set; }
        public string RelationshipTypeId { get; set; }
        public bool? IsIndex { get; set; }
        public Guid ClientId { get; set; }

        public RelationshipInfo()
        {
        }

        public RelationshipInfo(Guid relatedClientId, string relationshipTypeId, Guid clientId)
        {
            RelatedClientId = relatedClientId;
            RelationshipTypeId = relationshipTypeId;
            ClientId = clientId;
        }
    }
}