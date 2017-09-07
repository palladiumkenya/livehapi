using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class RelationshipType:Entity<string>
    {
        [Key]
        [MaxLength(50)]
        public override string Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<ClientRelationship> ClientRelationships { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}