using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Provider:Entity<Guid>
    {
        
        public string ProviderTypeId { get; set; }
        public string Code { get; set; }
        
        public Guid PracticeId { get; set; }
        
        public Guid PersonId { get; set; }
        
        public Person Person { get; set; }
        public Provider()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}