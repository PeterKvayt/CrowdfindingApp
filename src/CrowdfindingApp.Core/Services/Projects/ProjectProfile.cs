using System.Linq;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Project;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Core.Services.Projects
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectFilterInfo, ProjectFilter>();

            CreateMap<DraftProjectInfo, Project>();

            CreateMap<Project, ProjectInfo>().ReverseMap();               
        }
    }
}
