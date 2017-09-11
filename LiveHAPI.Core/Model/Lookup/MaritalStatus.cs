using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Lookup
{
    public class MaritalStatus:Entity<string>, IMaritalStatus
    {
        [Key]
        [MaxLength(50)]
        public override string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}