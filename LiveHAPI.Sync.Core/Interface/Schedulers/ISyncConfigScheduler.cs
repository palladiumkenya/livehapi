namespace LiveHAPI.Sync.Core.Interface.Schedulers
{
    public interface ISyncConfigScheduler
    {
        void Run();
        void Shutdown();
    }
}