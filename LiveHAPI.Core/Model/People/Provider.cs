using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class Provider:Entity<Guid>
    {
        
        [MaxLength(50)]
        public string Code { get; set; }
        public Guid PracticeId { get; set; }
        public Guid PersonId { get; set; }
        [MaxLength(50)]
        public string ProviderTypeId { get; set; }
        public Provider()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}