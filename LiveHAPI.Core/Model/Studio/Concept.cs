using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Studio
{
    public class Concept:Entity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string ConceptTypeId { get; set; }
        public Guid? CategoryId { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
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