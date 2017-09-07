using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Studio
{
    public class EncounterType : Entity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Program> Programs { get; set; }=new List<Program>();

        public EncounterType()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}