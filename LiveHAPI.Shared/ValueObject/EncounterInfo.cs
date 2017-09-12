using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class EncounterInfo
    {
        public Guid ClientId { get; set; }

        public Guid FormId { get; set; }

        public Guid EncounterTypeId { get; set; }
        public DateTime EncounterDate { get; set; }

        public Guid ProviderId { get; set; }

        public Guid DeviceId { get; set; }

        public Guid PracticeId { get; set; }

        public DateTime? Started { get; set; }
        public DateTime? Stopped { get; set; }
    }
}