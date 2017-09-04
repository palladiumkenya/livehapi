using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Validator:Entity<string>
    {
        public string Name { get; set; }
        public decimal Rank { get; set; }
    }
}