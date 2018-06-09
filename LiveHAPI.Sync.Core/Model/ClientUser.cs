
namespace LiveHAPI.Sync.Core.Model
{
    public class ClientUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }

        public override string ToString()
        {
            return $"{UserName} ({UserLastName} {UserFirstName})";
        }
    }
}
