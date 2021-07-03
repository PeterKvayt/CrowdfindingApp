using System;
using CrowdfindingApp.Common.Core.DataTransfers.Projects;
using CrowdfindingApp.Common.Enums;
using CrowdfindingApp.Common.Data.BusinessModels;

namespace CrowdfindingApp.Common.Core.Extensions
{
    public static class ProjectExtensions
    {
        public static string GetRestTime(this Project project)
        {
            return GetRestTime(project.StartDateTime, project.Duration);
        }

        public static string GetRestTime(this ProjectInfoView project)
        {
            return GetRestTime(project.StartDateTime, project.Duration);
        }

        private static string GetRestTime(DateTime? startDateTime, int? duration)
        {
            var restTime = startDateTime + new TimeSpan(duration.Value, 0, 1, 0, 0) - DateTime.UtcNow;
            if(restTime.Value.Days > 0)
            {
                return $"{restTime.Value.Days} д.";
            }

            if(restTime.Value.Hours > 1)
            {
                return $"{restTime.Value.Hours} ч.";
            }

            if(restTime.Value.Minutes > 1)
            {
                return $"{restTime.Value.Minutes} м.";
            }
            else
            {
                return $"< 1 м.";
            }
        }
    }
}
