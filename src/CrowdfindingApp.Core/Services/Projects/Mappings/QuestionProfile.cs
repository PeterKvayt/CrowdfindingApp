using AutoMapper;
using CrowdfindingApp.Common.Core.DataTransfers.Questions;
using CrowdfindingApp.Common.Core.Mappings;
using CrowdfindingApp.Common.Data.BusinessModels;

namespace CrowdfindingApp.Core.Services.Projects.Mappings
{
    public class QuestionProfile : ProfileBase<Question>
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
