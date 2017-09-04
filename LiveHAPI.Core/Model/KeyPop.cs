using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class KeyPop:Entity<string>
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}