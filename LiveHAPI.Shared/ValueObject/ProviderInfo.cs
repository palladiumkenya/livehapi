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
        public Identity Identity { get; set; }=new Identity();
        public PersonInfo PersonInfo { get; set; }=new PersonInfo();

        public ProviderInfo()
        {
        }
        public ProviderInfo(Identity identity, PersonInfo personInfo)
        {
            Identity = identity;
            PersonInfo = personInfo;
        }
        public ProviderInfo(string code, Identity identity, PersonInfo personInfo):this(identity,personInfo)
        {
            Code = code;
        }
        public ProviderInfo(string initials, string code, string providerTypeId, int? phone, string email, Identity identity, PersonInfo personInfo) :this(code,identity,personInfo)
        {
            Initials = initials;
            ProviderTypeId = providerTypeId;
            Phone = phone;
            Email = email;
        }
    }
}