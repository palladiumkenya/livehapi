using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class RelationshipType:Entity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}