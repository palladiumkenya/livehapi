using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class EncounterType : Entity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public EncounterType()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}