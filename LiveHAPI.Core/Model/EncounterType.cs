using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class EncounterType : Entity<Guid>
    {
        public string Name { get; set; }

        public EncounterType()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}