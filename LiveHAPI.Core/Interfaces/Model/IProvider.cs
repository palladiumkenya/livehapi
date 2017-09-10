namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IProvider
    {
        string Initials { get; set; }
        string Code { get; set; }
        string ProviderTypeId { get; set; }
        int? Phone { get; set; }
        string Email { get; set; }
        string Source { get; set; }
        string SourceRef { get; set; }
        string SourceSys { get; set; }
    }
}