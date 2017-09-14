using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class ConditionInfo : ICondition
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}