namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IValidator
    {
        string Id { get; set; }
        string Name { get; set; }
        decimal Rank { get; set; }
    }
}