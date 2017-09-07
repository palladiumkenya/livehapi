using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Encounters
{
    public class ObsFinalTestResult : Entity<Guid>
    {
        
        public Guid? FirstTestResult { get; set; }
        public string FirstTestResultCode { get; set; }
        
        public Guid? SecondTestResult { get; set; }
        public string SecondTestResultCode { get; set; }
        
        public Guid? FinalResult { get; set; }
        public string FinalResultCode { get; set; }
        
        public Guid? ResultGiven { get; set; }
        
        public Guid? CoupleDiscordant { get; set; }
        
        public Guid? SelfTestOption { get; set; }
        
        public Guid EncounterId { get; set; }


        public ObsFinalTestResult()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}