using System;

namespace LiveHAPI.Shared.ValueObject
{
    public class ProviderDTO
    {
        public Guid Id { get; set; }
        public string ProviderTypeId { get; set; }
        public string Code { get; set; }
        public Guid PracticeId { get; set; }
        public Guid PersonId { get; set; }
    }
}