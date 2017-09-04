using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Condition:Entity<string>
    {
        public string Name { get; set; }
    }
}