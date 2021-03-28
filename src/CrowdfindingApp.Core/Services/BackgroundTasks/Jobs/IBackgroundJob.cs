using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfindingApp.Core.Services.BackgroundTasks.Jobs
{
    public interface IBackgroundJob
    {
        Task Execute();
    }
}
