using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class IdentifierType : Entity<string>
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}