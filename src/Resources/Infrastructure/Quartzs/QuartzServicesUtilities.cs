using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Infrastructure.Quartzs
{
    //dotnet add package Microsoft.Extensions.DependencyInjection --version 2.2.0
    //dotnet add package Quartz --version 3.0.7
    public static class QuartzServicesUtilities
    {
        public static void UseQuartz(this IServiceCollection services, params Type[] jobs)
        {
            services.AddSingleton<IJobFactory, QuartzJonFactory>();
            services.Add(jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton)));

            services.AddSingleton(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });
        }

        public static void StartJob<TJob>(IScheduler scheduler, TimeSpan runInterval, 
                                                                DateTimeOffset startAt, 
                                                                DateTimeOffset endingAt, bool dailyTime = false) where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>().WithIdentity(jobName).Build();

            if (!dailyTime)
            {
                var trigger = TriggerBuilder.Create().WithIdentity($"{jobName}.trigger")
                                                    .StartAt(startAt).EndAt(endingAt)
                                                    .WithSimpleSchedule(scheduleBuilder =>
                                                                scheduleBuilder.WithInterval(runInterval)
                                                                               .RepeatForever())
                                                    .Build();
                scheduler.ScheduleJob(job, trigger);
            }
            else
            {
                var trigger = TriggerBuilder.Create().WithIdentity($"{jobName}.trigger")
                                                     .WithDailyTimeIntervalSchedule(scheduleBuilder => scheduleBuilder.WithIntervalInHours(2)
                                                     .OnEveryDay()
                                                     .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 0))
                                                     .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(21, 0)))
                                                     .Build();
                scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
