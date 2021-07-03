using CrowdfundingApp.Common.Core.DataTransfers.Project;
using CrowdfundingApp.Common.Core.DataTransfers.Projects;
using CrowdfundingApp.Common.Core.Mappings;
using CrowdfundingApp.Common.Data.BusinessModels;
using CrowdfundingApp.Common.Data.Filters;

namespace CrowdfundingApp.Core.Services.Projects.Mappings
{
    public class ProjectProfile : ProfileBase<Project>
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

            CreateMap<ProjectInfo, ProjectInfoView>();
        }
    }
}
