using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

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

        private Provider(string initials, string code, string providerTypeId, int? phone, string email)
        {
            Initials = initials;
            Code = code;
            ProviderTypeId = providerTypeId;
            Phone = phone;
            Email = email;
        }

        private Provider(string initials, string code, string providerTypeId, int? phone, string email, string source, string sourceRef, string sourceSys, Guid? practiceId):this(initials,code,providerTypeId,phone,email)
        {
            Source = source;
            SourceRef = sourceRef;
            SourceSys = sourceSys;
            PracticeId = practiceId;
        }

        public void ChangeTo(Provider provider)
        {
            Initials = provider.Initials;
            Code = provider.Code;
            ProviderTypeId = provider.ProviderTypeId;
            Phone = provider.Phone;
            Email = provider.Email;
        }

        public static Provider Create(ProviderInfo providerInfo, Guid practiceId)
        {
            return new Provider(providerInfo.Initials, providerInfo.Code,providerInfo.ProviderTypeId, providerInfo.Phone, providerInfo.Email,providerInfo.Identity.Source, providerInfo.Identity.SourceRef,providerInfo.Identity.SourceSys, practiceId);
        }

        public override string ToString()
        {
            return $"{Initials}|{Code}|{ProviderTypeId}";
        }
    }
}