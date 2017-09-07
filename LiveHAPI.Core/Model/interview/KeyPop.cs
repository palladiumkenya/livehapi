using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class KeyPop:Entity<string>
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}