using AutoMapper;
using CrowdfundingApp.Common.Core.DataTransfers.Questions;
using CrowdfundingApp.Common.Core.Mappings;
using CrowdfundingApp.Common.Data.BusinessModels;

namespace CrowdfundingApp.Core.Services.Projects.Mappings
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
