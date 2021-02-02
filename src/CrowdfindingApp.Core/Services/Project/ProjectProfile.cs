using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Project;
using CrowdfindingApp.Data.Common.Filters;

namespace CrowdfindingApp.Core.Services.Project
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectFilterInfo, ProjectFilter>();
        }
    }
}
