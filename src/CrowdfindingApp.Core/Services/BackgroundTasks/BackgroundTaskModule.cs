using System;
using Autofac;
using CrowdfindingApp.Core.Services.BackgroundTasks.Jobs;
using Hangfire;

namespace CrowdfindingApp.Core.Services.BackgroundTasks
{
    public class BackgroundTaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //RegisterJob<UpdateStatusOfExpiriedProjectsJob>(builder, Cron.Hourly, nameof(UpdateStatusOfExpiriedProjectsJob));
            //RegisterJob<ClearExpiredTempFilesJob>(builder, Cron.Daily, nameof(ClearExpiredTempFilesJob));
        }

        private void RegisterJob<JobType>(ContainerBuilder builder, Func<string> frequency, string name) where JobType: IBackgroundJob
        {
            builder.RegisterType<JobType>().AsSelf().SingleInstance();
            RecurringJob.AddOrUpdate<JobType>(name, job => job.Execute(), frequency);
        }
    }
}
