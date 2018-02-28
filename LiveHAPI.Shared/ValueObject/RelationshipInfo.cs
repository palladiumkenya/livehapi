using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class RelationshipInfo : IClientRelationship
    {
        public Guid Id { get; set; }
        public Guid RelatedClientId { get; set; }
        public string RelationshipTypeId { get; set; }
        public bool? IsIndex { get; set; }
        public Guid ClientId { get; set; }

        public bool HasIndexClient()
        {
            return null != IsIndex && IsIndex.HasValue && IsIndex.Value;
        }

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