using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Topshelf;

namespace LiveHAPI.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile("logs\\service\\log-{Date}.txt")
                .CreateLogger();

            Log.Debug("initializing LiveHAPI service...");

            var rc = HostFactory.Run(x =>                                  
            {
                x.Service<AppsService>(s =>                                  
                {
                    s.ConstructUsing(name => new AppsService());                
                    s.WhenStarted(tc => tc.Start());                         
                    s.WhenStopped(tc => tc.Stop());                          
                });

                x.StartAutomatically();
                x.RunAsLocalSystem();                                       

                x.SetDescription("Afya Mobile API service");                  
                x.SetDisplayName("LiveHAPI Service");                                 
                x.SetServiceName("LiveHAPI");                                  
            });                                                            

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  
            Environment.ExitCode = exitCode;
        }
    }
}
