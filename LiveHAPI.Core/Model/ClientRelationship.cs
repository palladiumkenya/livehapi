using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class ClientRelationship : Entity<Guid>
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
    }
}