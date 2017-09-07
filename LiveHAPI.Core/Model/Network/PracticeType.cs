using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Network
{
    public class PracticeType:Entity<string>, IPracticeType
    {
        [Key]
        [MaxLength(50)]
        public override string Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        public ICollection<Practice> Practices { get; set; }
      
    }
}