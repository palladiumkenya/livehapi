using System;
using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Serilog;

namespace LiveHAPI.Sync.Jobs
{
    [DisallowConcurrentExecution]
    public class SyncFacilitiesJob : IJob
    {
        public  Task Execute(IJobExecutionContext context)
        {
            Log.Debug($"<<< {nameof(SyncFacilitiesJob).ToUpper()} >>>");
            try
            {
                var service = Program.ServiceProvider.GetService<ISyncFacilityService>();
                var count =  service.Sync().GetAwaiter().GetResult();;
                service.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error($"error executing {nameof(SyncFacilitiesJob)} job");
                Log.Error($"{ex}");
             // JobExecutionException qe = new JobExecutionException(ex);
             // qe.RefireImmediately = true; // this job will refire immediately
            }
            return Task.CompletedTask;
        }
    }
}
