using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Encounters
{
    public class Obs:Entity<Guid>
    {
        public Guid QuestionId { get; set; }
        public DateTime ObsDate { get; set; }        
        public string ValueText { get; set; }
        public decimal? ValueNumeric { get; set; }
        public Guid? ValueCoded { get; set; }
        public string ValueMultiCoded { get; set; }
        public DateTime? ValueDateTime { get; set; }
        
        public Guid EncounterId { get; set; }
        
        public bool IsNull { get; set; }

        public Obs()
        {
            Id = LiveGuid.NewGuid();
            ObsDate=DateTime.Now;
        }
    }
}