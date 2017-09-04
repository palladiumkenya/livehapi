using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Form:Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        
        public Guid ModuleId { get; set; }
        
        public List<Question> Questions { get; set; }=new List<Question>();

        
        public List<Program> Programs { get; set; } = new List<Program>();
        
        public Guid DefaultEncounterTypeId { get; set; }

        
        public List<Encounter> ClientEncounters { get; set; }=new List<Encounter>();



        public Form()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Display}";
        }
    }
}