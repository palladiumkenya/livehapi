using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class Provider:Entity<Guid>, IProvider
    {
        [MaxLength(50)]
        public string Initials { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string ProviderTypeId { get; set; }
        public int? Phone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
        [MaxLength(50)]
        public string SourceRef { get; set; }
        [MaxLength(50)]
        public string SourceSys { get; set; }
        public Guid? PracticeId { get; set; }
        public Guid PersonId { get; set; }
        

        public Provider()
        {
            Id = LiveGuid.NewGuid();
        }

        public void ChangeTo(Provider provider)
        {
            Initials = provider.Initials;
            Code = provider.Code;
            ProviderTypeId = provider.ProviderTypeId;
            Phone = provider.Phone;
            Email = provider.Email;
        }
    }
}