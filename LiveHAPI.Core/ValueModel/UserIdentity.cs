namespace LiveHAPI.Core.ValueModel
{
    public class UserIdentity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }

        public UserIdentity()
        {
        }

        public UserIdentity(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public UserIdentity(string userName, string password, int? phone, string email):this(userName,password)
        {
            Phone = phone;
            Email = email;
        }
    }
}