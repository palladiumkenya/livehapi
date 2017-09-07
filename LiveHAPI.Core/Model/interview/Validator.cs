using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Validator:Entity<string>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Rank { get; set; }
    }
}