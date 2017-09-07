using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Studio
{
    public class Form:Entity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Display { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public decimal Rank { get; set; }
        public Guid ModuleId { get; set; }

//        
//        public List<Question> Questions { get; set; }=new List<Question>();
//        public List<Program> Programs { get; set; } = new List<Program>();
//        public List<Encounter> ClientEncounters { get; set; }=new List<Encounter>();
        
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