using Serilog;

namespace LiveHAPI.Service
{
    public class AppsService
    {
        public void Start()
        {
            Log.Debug("starting LiveHAPI service...");
        }
        public void Stop()
        {
            Log.Debug("stopping LiveHAPI service...");
        }
    }
}