using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsTestResultInfo : IObsTestResult
    {
        public Guid Id { get; set; }
        public int Attempt { get; set; }
        public Guid EncounterId { get; set; }
        public DateTime Expiry { get; set; }
        public Guid Kit { get; set; }
        public string KitDisplay { get; set; }
        public string KitOther { get; set; }
        public string LotNumber { get; set; }
        public Guid Result { get; set; }
        public string ResultCode { get; set; }
        public string ResultDisplay { get; set; }
        public string TestName { get; set; }
    }
}