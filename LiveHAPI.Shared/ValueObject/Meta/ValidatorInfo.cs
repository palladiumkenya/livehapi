using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class ValidatorInfo : IValidator
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Rank { get; set; }
    }
}