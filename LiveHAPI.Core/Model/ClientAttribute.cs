using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class ClientAttribute:Entity<string>
    {
        public string Name { get; set; }
    }
}