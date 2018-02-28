using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObsFinalTestResult
    {
        Guid? CoupleDiscordant { get; set; }
        Guid ClientId { get; set; }
        Guid EncounterId { get; set; }
        Guid? FinalResult { get; set; }
        string FinalResultCode { get; set; }
        Guid? FirstTestResult { get; set; }
        string FirstTestResultCode { get; set; }
        Guid? ResultGiven { get; set; }
        Guid? SecondTestResult { get; set; }
        string SecondTestResultCode { get; set; }
        Guid? SelfTestOption { get; set; }
    }
}