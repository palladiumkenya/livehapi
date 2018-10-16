using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using LiveHAPI.Sync.Core.Interface.Schedulers;
using LiveHAPI.Sync.Jobs;
using Quartz;
using Quartz.Impl;
using Serilog;

namespace LiveHAPI.Sync.Schedulers
{
    public class SyncConfigScheduler : ISyncConfigScheduler
    {
        private readonly int _configInterval;
        private readonly int _clientInterval;
        private IScheduler _scheduler;

        public SyncConfigScheduler(string configPeriod, string clientPeriod)
        {
            if (configPeriod.EndsWith("hrs"))
            {
                bool result = Int32.TryParse(configPeriod.Replace("hrs", "").Trim(), out var number);
                _configInterval = result ? number : 24;
            }

            if (clientPeriod.EndsWith("secs"))
            {
                bool result2 = Int32.TryParse(configPeriod.Replace("secs", "").Trim(), out var number2);
                _clientInterval = result2 ? number2 : 15;
                if (_clientInterval < 61)
                {
                    _clientInterval = 60;
                }
            }
        }

        public async void Run()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.scheduler.instanceName","LiveHAPI" },
                { "quartz.threadPool.type","Quartz.Simpl.SimpleThreadPool, Quartz" },
                { "quartz.threadPool.threadCount","1" },
                { "quartz.threadPool.threadPriority","Normal" },
                { "quartz.jobStore.type","Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.jobStore.misfireThreshold","60000" }
            };

            StdSchedulerFactory factory = new StdSchedulerFactory(props);
           _scheduler = await factory.GetScheduler();

            await _scheduler.Start();

            Log.Debug("Sync started !");

            var jobs = new List<Type>
            {
                typeof(SyncFacilitiesJob),
                typeof(SyncUsersJob),
                typeof(SyncLookupsJob),
                typeof(ExtractClientsJob),
                typeof(SyncClientsJob)
            };

            foreach (var job in jobs)
                await _scheduler.ScheduleJob(GetJobDetail(job), GeTrigger(job));
        }

        private IJobDetail GetJobDetail(Type jobType)
        {
            string jobName = $"j{jobType.Name}";
            string jobGroup = $"j{jobType.Name}group";

            var job = JobBuilder.Create(jobType)
                .WithIdentity($"{jobName}", $"{jobGroup}")
                .Build();

            return job;
        }
        private ITrigger GeTrigger(Type jobType)
        {
            string triggerName = $"t{jobType.Name}";
            string triggerGroup = $"t{jobType.Name}group";

            if (jobType == typeof(SyncClientsJob)||jobType ==typeof(ExtractClientsJob))
            {
                return TriggerBuilder.Create()
                    .WithIdentity($"{triggerName}", $"{triggerGroup}")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(_clientInterval)
                        .RepeatForever())
                    .Build();
            }
            else
            {
                return TriggerBuilder.Create()
                    .WithIdentity($"{triggerName}", $"{triggerGroup}")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(_configInterval)
                        .RepeatForever())
                    .Build();
            }
        }

        public async void Shutdown()
        {
           await _scheduler.Shutdown(true);
        }
    }
}