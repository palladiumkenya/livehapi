using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class ActionInfo : IAction
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}