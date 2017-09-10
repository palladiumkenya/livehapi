namespace LiveHAPI.Shared.ValueObject
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public Identity Identity { get; set; }=new Identity();
        public PersonInfo PersonInfo { get; set; }=new PersonInfo();

        public UserInfo()
        {
        }

        public UserInfo(string userName, string password, Identity identity, PersonInfo personInfo)
        {
            UserName = userName;
            Password = password;
            Identity = identity;
            PersonInfo = personInfo;
        }

        public UserInfo(string userName, string password, int? phone, string email, Identity identity,
            PersonInfo personInfo) : this(userName, password, identity, personInfo)
        {
            Phone = phone;
            Email = email;
        }
    }
}