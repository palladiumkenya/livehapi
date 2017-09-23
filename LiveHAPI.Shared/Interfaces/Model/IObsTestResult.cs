using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObsTestResult
    {
        int Attempt { get; set; }
        Guid EncounterId { get; set; }
        DateTime Expiry { get; set; }
        Guid Kit { get; set; }
        string KitDisplay { get; set; }
        string KitOther { get; set; }
        string LotNumber { get; set; }
        Guid Result { get; set; }
        string ResultCode { get; set; }
        string ResultDisplay { get; set; }
        string TestName { get; set; }
    }
}