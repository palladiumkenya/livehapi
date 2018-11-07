using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{

    public class KeyPopInfo: IKeyPop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}