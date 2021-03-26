using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdfindingApp.Core.Services.BackgroundTasks.Jobs;
using Hangfire;

namespace CrowdfindingApp.Core.Services.BackgroundTasks
{
    public class BackgroundTaskRegistrator
    {
        public void Register()
        {
            RecurringJob.AddOrUpdate<UpdateStatusOfExpiriedProjectsJob>(nameof(UpdateStatusOfExpiriedProjectsJob), job => job.Execute(), Cron.Hourly);
        }
    }
}
