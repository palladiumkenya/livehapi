using System;
using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Serilog;

namespace LiveHAPI.Sync.Jobs
{
    [DisallowConcurrentExecution]
    public class ExtractClientsJob : IJob
    {
        public  Task Execute(IJobExecutionContext context)
        {
            Log.Debug($"<<< {nameof(ExtractClientsJob).ToUpper()} >>>");
            try
            {
                var service = Program.ServiceProvider.GetService<IExtractClientsService>();
                var count =  service.Sync().GetAwaiter().GetResult();;
                service.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error($"error executing {nameof(ExtractClientsJob)} job");
                Log.Error($"{ex}");
//              JobExecutionException qe = new JobExecutionException(ex);
//              qe.RefireImmediately = true; // this job will refire immediately
            }
            return Task.CompletedTask;
        }
    }
}
