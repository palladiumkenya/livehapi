namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface ISourceIdentity
    {
        string Source { get; set; }
        string SourceRef { get; set; }
        string SourceSys { get; set; }
    }
}