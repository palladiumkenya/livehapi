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

        
        public string Status
        {
            get
            {
                if (IsComplete)
                    return "Completed";

                return "Started";
            }
        }

        
        public bool HasObs
        {
            get { return Obses.Any(); }
        }
      


        public Encounter()
        {
            //Status = "Created";
            Id = LiveGuid.NewGuid();
            EncounterDate = DateTime.Now;
        }
        public Encounter(Guid formId, Guid encounterTypeId, Guid clientId,  Guid providerId, Guid userId):this()
        {
            FormId = formId;
            EncounterTypeId = encounterTypeId;
            ClientId = clientId;
            ProviderId = providerId;
            UserId = userId;
        }
        public static Encounter CreateNew(Guid formId, Guid encounterTypeId, Guid clientId, Guid providerId, Guid userId)
        {
            var encounter = new Encounter(formId,encounterTypeId, clientId, providerId, userId);
            return encounter;
        }

        public void AddOrUpdate(Obs obs,bool saveNulls=true)
        {
            var obses = Obses.ToList();

            if (obses.Any(x => x.QuestionId == obs.QuestionId))
            {
                var obsForUpdate = obses.First(x => x.QuestionId == obs.QuestionId);
                obses.Remove(obsForUpdate);
                if (saveNulls)
                {
                    obsForUpdate.UpdateFrom(obs);
                    obses.Add(obsForUpdate);
                }
                else
                {
                    if (!obs.IsNull)
                    {
                        obsForUpdate.UpdateFrom(obs);
                        obses.Add(obsForUpdate);
                    }
                }
            }
            else
            {
                obs.EncounterId = Id;
                if (saveNulls)
                {
                    obses.Add(obs);
                }
                else
                {
                    if(!obs.IsNull)
                        obses.Add(obs);
                }
            }

            Obses = obses;
        }

        public override string ToString()
        {
            return $"{Id} {EncounterDate:F} [{Status}]";
        }
    }
}