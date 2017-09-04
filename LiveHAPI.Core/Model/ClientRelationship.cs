using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class ClientRelationship : Entity<Guid>
    {
        
        public string RelationshipTypeId { get; set; }
        
        public Guid RelatedClientId { get; set; }
        public bool Preferred { get; set; }
        
        public Guid ClientId { get; set; }
        
        public  Person Person { get; set; }

        public ClientRelationship()
        {
            Id = LiveGuid.NewGuid();
        }

        private ClientRelationship(string relationshipTypeId, Guid relatedClientId, bool preferred, Guid clientId):this()
        {
            RelationshipTypeId = relationshipTypeId;
            RelatedClientId = relatedClientId;
            Preferred = preferred;
            ClientId = clientId;
        }

        public static ClientRelationship Create(string relationshipTypeId, Guid relatedClientId, bool preferred, Guid clientId)
        {
            return new ClientRelationship(relationshipTypeId, relatedClientId, preferred,clientId);
        }

        public override string ToString()
        {
            string person = null != Person ? Person.ToString() : string.Empty;
            return $"{RelationshipTypeId}|{RelatedClientId} - {person}";
        }
    }
}