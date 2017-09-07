using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsTraceResult : Entity<Guid>
    {
        public DateTime Date { get; set; }
        
        public Guid Mode { get; set; }
        
        public Guid Outcome { get; set; }
        public Guid EncounterId { get; set; }

        public ObsTraceResult()
        {
            Id = LiveGuid.NewGuid();
        }

       


    }
}