using System;
using System.Collections.Generic;
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
        private readonly int _interval;
        private IScheduler _scheduler;

        public SyncConfigScheduler(string period)
        {
            bool result = Int32.TryParse(period, out var number);
            _interval = result ? number : 15;
        }

        public async void Run()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
           _scheduler = await factory.GetScheduler();

            await _scheduler.Start();

            Log.Debug("Sync started !");

            var jobs = new List<Type>
            {
                typeof(SyncFacilitiesJob),
                typeof(SyncUsersJob)
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

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{triggerName}", $"{triggerGroup}")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(_interval)
                    .RepeatForever())
                .Build();

            return trigger;
        }

        public async void Shutdown()
        {
           await _scheduler.Shutdown(true);
        }
    }
}