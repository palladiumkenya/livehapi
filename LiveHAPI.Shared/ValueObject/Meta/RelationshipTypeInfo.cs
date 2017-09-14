using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class RelationshipTypeInfo: IRelationshipType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}