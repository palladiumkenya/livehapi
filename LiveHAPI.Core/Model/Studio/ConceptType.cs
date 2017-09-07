using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Studio
{
    public class ConceptType:Entity<string>
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}