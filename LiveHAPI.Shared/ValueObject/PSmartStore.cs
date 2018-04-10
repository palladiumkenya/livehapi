using System;

namespace LiveHAPI.Shared.ValueObject
{
    public class PSmartStoreInfo
    {
        public Guid Id { get; set; }
        public string Shr { get; set; }
        public DateTime? Date_Created { get; set; }
        public string Status { get; set; }
        public DateTime? Status_Date { get; set; }
        public Guid Uuid { get; set; }
    }
}