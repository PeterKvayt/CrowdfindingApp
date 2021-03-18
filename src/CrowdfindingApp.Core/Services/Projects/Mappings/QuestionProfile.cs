using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Questions;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Core.Services.Projects.Mappings
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionInfo, Question>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Question))
                .ReverseMap()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Description));
        }
    }
}
