using System;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Concept:Entity<Guid>
    {
        public string Name { get; set; }
        
        public string ConceptTypeId { get; set; }
        
        public Guid? CategoryId { get; set; }
        
        public Category Category { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}