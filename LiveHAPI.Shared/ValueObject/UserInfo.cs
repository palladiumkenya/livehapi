namespace LiveHAPI.Shared.ValueObject
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public SourceIdentity SourceIdentity { get; set; }=new SourceIdentity();
        public PersonNameInfo PersonNameInfo { get; set; }=new PersonNameInfo();

        public UserInfo()
        {
        }

        public UserInfo(string userName, string password, SourceIdentity sourceIdentity, PersonNameInfo personNameInfo)
        {
            UserName = userName;
            Password = password;
            SourceIdentity = sourceIdentity;
            PersonNameInfo = personNameInfo;
        }

        public UserInfo(string userName, string password, int? phone, string email, SourceIdentity sourceIdentity,
            PersonNameInfo personNameInfo) : this(userName, password, sourceIdentity, personNameInfo)
        {
            Phone = phone;
            Email = email;
        }
    }
}