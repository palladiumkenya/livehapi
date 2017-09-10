namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IContact
    {
        int Phone { get; set; }
        string Source { get; set; }
        string SourceRef { get; set; }
        string SourceSys { get; set; }
    }
}