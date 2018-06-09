using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class TempClientRelationship : Entity<Guid>, IClientRelationship
    {
        public Guid RelatedClientId { get; set; }
        [MaxLength(50)]
        public string RelationshipTypeId { get; set; }
        public bool Preferred { get; set; }
        public Guid ClientId { get; set; }
        public bool? IsIndex { get; set; }

        public TempClientRelationship()
        {
        }

        private TempClientRelationship(Guid id, Guid relatedClientId, string relationshipTypeId, bool preferred, Guid clientId, bool? isIndex) : base(id)
        {
            RelatedClientId = relatedClientId;
            RelationshipTypeId = relationshipTypeId;
            Preferred = preferred;
            ClientId = clientId;
            IsIndex = isIndex;
        }

        public static TempClientRelationship Create(ClientRelationship clientRelationship)
        {
            return new TempClientRelationship(
                clientRelationship.Id, 
                clientRelationship.RelatedClientId,
                clientRelationship.RelationshipTypeId, 
                clientRelationship.Preferred, 
                clientRelationship.ClientId,
                clientRelationship.IsIndex);
        }
    }
}