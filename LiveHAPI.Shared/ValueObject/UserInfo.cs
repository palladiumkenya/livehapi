namespace LiveHAPI.Shared.ValueObject
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        Identity Identity { get; set; }

        public UserInfo()
        {
        }

        public UserInfo(string userName, string password,Identity identity)
        {
            UserName = userName;
            Password = password;
            Identity = identity;
        }

        public UserInfo(string userName, string password, int? phone, string email, Identity identity) :this(userName,password,identity)
        {
            Phone = phone;
            Email = email;
        }
    }
}