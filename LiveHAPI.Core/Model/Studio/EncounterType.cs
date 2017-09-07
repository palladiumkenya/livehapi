using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Studio
{
    public class EncounterType : Entity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<FormProgram> Programs { get; set; }=new List<FormProgram>();
        public ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();
        public EncounterType()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}