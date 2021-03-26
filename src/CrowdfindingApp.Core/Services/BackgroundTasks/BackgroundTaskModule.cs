using Autofac;
using CrowdfindingApp.Core.Services.BackgroundTasks.Jobs;
using Hangfire;

namespace CrowdfindingApp.Core.Services.BackgroundTasks
{
    public class BackgroundTaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UpdateStatusOfExpiriedProjectsJob>().AsSelf().SingleInstance();
            RecurringJob.AddOrUpdate<UpdateStatusOfExpiriedProjectsJob>(nameof(UpdateStatusOfExpiriedProjectsJob), job => job.Execute(), Cron.Hourly);
        }
    }
}
