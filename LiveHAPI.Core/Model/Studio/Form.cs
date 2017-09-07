using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Studio
{
    public class Form:Entity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Version { get; set; }
        [MaxLength(50)]
        public string Display { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public decimal Rank { get; set; }
        public Guid ModuleId { get; set; }
        public ICollection<FormImplementation> Implementations { get; set; }=new List<FormImplementation>();
        public ICollection<FormProgram> Programs { get; set; } = new List<FormProgram>();
        public ICollection<Question> Questions { get; set; }=new List<Question>();
        public ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();

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