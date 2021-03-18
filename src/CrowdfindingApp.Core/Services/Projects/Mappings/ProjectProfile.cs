using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Project;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Filters;

namespace CrowdfindingApp.Core.Services.Projects.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectFilterInfo, ProjectFilter>();

            CreateMap<Project, ProjectInfo>().ReverseMap(); 
            
            CreateMap<Project, ProjectCard>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.ImgPath, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Budget));
        }
    }
}
