using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Encounter:Entity<Guid>
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

        
        public IEnumerable<Obs> Obses { get; set; } = new List<Obs>();
        
        public IEnumerable<ObsTestResult> ObsTestResults { get; set; } = new List<ObsTestResult>();
        
        public IEnumerable<ObsFinalTestResult> ObsFinalTestResults { get; set; } = new List<ObsFinalTestResult>();
        
        public IEnumerable<ObsTraceResult> ObsTraceResults { get; set; } = new List<ObsTraceResult>();
        
        public IEnumerable<ObsLinkage> ObsLinkages { get; set; } = new List<ObsLinkage>();
        
        public Guid UserId { get; set; }
        public bool IsComplete { get; set; }
        
        public Encounter()
        {
            Id = LiveGuid.NewGuid();
            EncounterDate = DateTime.Now;
        }
    }
}