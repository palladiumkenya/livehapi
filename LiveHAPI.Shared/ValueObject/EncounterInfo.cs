using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class EncounterInfo : IEncounter
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }

        public Guid FormId { get; set; }

        public Guid EncounterTypeId { get; set; }
        public DateTime EncounterDate { get; set; }

        public Guid ProviderId { get; set; }

        public Guid DeviceId { get; set; }
        public string PracticeCode { get; set; }
        public Guid PracticeId { get; set; }

        public DateTime? Started { get; set; }
        public DateTime? Stopped { get; set; }
        public Guid UserId { get; set; }

        public List<ObsInfo> Obses { get; set; }=new List<ObsInfo>();
        public List<ObsTestResultInfo> ObsTestResults { get; set; } = new List<ObsTestResultInfo>();
        public List<ObsFinalTestResultInfo> ObsFinalTestResults { get; set; } = new List<ObsFinalTestResultInfo>();
        public List<ObsTraceResultInfo> ObsTraceResults { get; set; } = new List<ObsTraceResultInfo>();
        public List<ObsLinkageInfo> ObsLinkages { get; set; } = new List<ObsLinkageInfo>();

    }
}