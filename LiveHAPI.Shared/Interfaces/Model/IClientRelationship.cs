using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IClientRelationship
    {
        Guid RelatedClientId { get; set; }
        string RelationshipTypeId { get; set; }
        Guid ClientId { get; set; }
    }
}