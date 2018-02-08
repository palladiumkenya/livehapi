using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class Encounter:Entity<Guid>,IEncounter
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
        public Guid UserId { get; set; }

        public ICollection<Obs> Obses { get; set; } = new List<Obs>();
        public ICollection<ObsTestResult> ObsTestResults { get; set; } = new List<ObsTestResult>();
        public ICollection<ObsFinalTestResult> ObsFinalTestResults { get; set; } = new List<ObsFinalTestResult>();
        public ICollection<ObsTraceResult> ObsTraceResults { get; set; } = new List<ObsTraceResult>();
        public ICollection<ObsLinkage> ObsLinkages { get; set; } = new List<ObsLinkage>();

        public ICollection<ObsMemberScreening> ObsMemberScreenings { get; set; } = new List<ObsMemberScreening>();
        public ICollection<ObsPartnerScreening> ObsPartnerScreenings { get; set; } = new List<ObsPartnerScreening>();
        public ICollection<ObsFamilyTraceResult> ObsFamilyTraceResults { get; set; } = new List<ObsFamilyTraceResult>();
        public ICollection<ObsPartnerTraceResult> ObsPartnerTraceResults { get; set; } = new List<ObsPartnerTraceResult>();

        public bool IsComplete { get; set; }
        
        public Encounter()
        {
            Id = LiveGuid.NewGuid();
            EncounterDate = DateTime.Now;
        }

        private Encounter(Guid id, 
            Guid clientId, Guid formId, Guid encounterTypeId, DateTime encounterDate, Guid providerId, Guid deviceId, Guid practiceId, DateTime? started, DateTime? stopped, Guid userId,bool isComplete) 
            : base(id)
        {
            ClientId = clientId;
            FormId = formId;
            EncounterTypeId = encounterTypeId;
            EncounterDate = encounterDate;
            ProviderId = providerId;
            DeviceId = deviceId;
            PracticeId = practiceId;
            Started = started;
            Stopped = stopped;
            UserId = userId;
            IsComplete = isComplete;
        }

        private Encounter(Guid clientId, Guid formId, Guid encounterTypeId, DateTime encounterDate, Guid providerId, Guid deviceId, Guid practiceId, DateTime? started, DateTime? stopped, Guid userId, bool isComplete)
            :this(LiveGuid.NewGuid(), clientId,formId,encounterTypeId,encounterDate,providerId,deviceId,practiceId,started,stopped,userId,  isComplete)
        {           
        }
    
        public static Encounter Create(EncounterInfo encounterInfo)
        {
            return new Encounter(encounterInfo.Id, encounterInfo.ClientId,encounterInfo.FormId,encounterInfo.EncounterTypeId,encounterInfo.EncounterDate, encounterInfo.ProviderId, encounterInfo.DeviceId, encounterInfo.PracticeId, encounterInfo.Started, encounterInfo.Stopped,encounterInfo.UserId,encounterInfo.IsComplete);
        }

        public void Update(EncounterInfo encounterInfo)
        {
            ClientId = encounterInfo.ClientId;
            FormId = encounterInfo.FormId;
            EncounterTypeId = encounterInfo.EncounterTypeId;
            EncounterDate = encounterInfo.EncounterDate;
            ProviderId = encounterInfo.ProviderId;
            DeviceId = encounterInfo.DeviceId;
            PracticeId = encounterInfo.PracticeId;
            Started = encounterInfo.Started;
            Stopped = encounterInfo.Stopped;
            IsComplete = encounterInfo.IsComplete;
            UserId = encounterInfo.UserId;
        }

        public override string ToString()
        {
            return $"{Id}|{EncounterDate:yyyy-MMM-dd ddd}|{ClientId}";
        }
    }
}