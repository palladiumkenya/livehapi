using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class RelationshipInfo : IClientRelationship
    {
        public Guid RelatedClientId { get; set; }
        public string RelationshipTypeId { get; set; }
        public Guid ClientId { get; set; }
    }
}