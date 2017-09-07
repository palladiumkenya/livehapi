using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class PracticeType:Entity<string>, IPracticeType
    {
        [Key]
        [MaxLength(50)]
        public override string Id { get; set; }
        public ICollection<Practice> Practices { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public PracticeType()
        {
        }
    }
}