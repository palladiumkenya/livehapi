using System.ComponentModel.DataAnnotations;

namespace LiveHAPI.Shared.ValueObject
{
    public class ProviderInfo
    {
        public string Initials { get; set; }
        public string Code { get; set; }
        public string ProviderTypeId { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public SourceIdentity SourceIdentity { get; set; }=new SourceIdentity();
        public PersonNameInfo PersonNameInfo { get; set; }=new PersonNameInfo();

        public ProviderInfo()
        {
        }
        public ProviderInfo(SourceIdentity sourceIdentity, PersonNameInfo personNameInfo)
        {
            SourceIdentity = sourceIdentity;
            PersonNameInfo = personNameInfo;
        }
        public ProviderInfo(string code, SourceIdentity sourceIdentity, PersonNameInfo personNameInfo):this(sourceIdentity,personNameInfo)
        {
            Code = code;
        }
        public ProviderInfo(string initials, string code, string providerTypeId, int? phone, string email, SourceIdentity sourceIdentity, PersonNameInfo personNameInfo) :this(code,sourceIdentity,personNameInfo)
        {
            Initials = initials;
            ProviderTypeId = providerTypeId;
            Phone = phone;
            Email = email;
        }
    }
}