using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class IdentifierType : Entity<string>
    {
        [Key]
        [MaxLength(50)]
        public override string Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<ClientIdentifier> ClientIdentifiers { get; set; }

        public IdentifierType()
        {
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}