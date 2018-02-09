using System;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsFinalTestResultInfo : IObsFinalTestResult
    {
        public Guid Id { get; set; }

        public Guid? CoupleDiscordant { get; set; }
        public Guid EncounterId { get; set; }
        public Guid? FinalResult { get; set; }
        public string FinalResultCode { get; set; }
        public Guid? FirstTestResult { get; set; }
        public string FirstTestResultCode { get; set; }
        public Guid? ResultGiven { get; set; }
        public Guid? SecondTestResult { get; set; }
        public string SecondTestResultCode { get; set; }
        public Guid? SelfTestOption { get; set; }
        public string Remarks { get; set; }
    }
}