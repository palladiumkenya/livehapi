using System;
using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Serilog;

namespace LiveHAPI.Sync.Jobs
{
    [DisallowConcurrentExecution]
    public class SyncLookupsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var service = Program.ServiceProvider.GetService<ISyncLookupService>();
                var count = await service.Sync();
            }
            catch (Exception ex)
            {
                Log.Error($"error executing {nameof(SyncLookupsJob)} job");
                Log.Error($"{ex}");

//                JobExecutionException qe = new JobExecutionException(ex);
//                qe.RefireImmediately = true; // this job will refire immediately
            }
        }
    }
}