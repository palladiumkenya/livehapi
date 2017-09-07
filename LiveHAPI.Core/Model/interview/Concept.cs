using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Concept:Entity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string ConceptTypeId { get; set; }
        
        public Guid? CategoryId { get; set; }
        
        public Category Category { get; set; }

        public Concept()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}