using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Data.BusinessModels;

namespace CrowdfundingApp.Common.Data.Interfaces.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task AddQuestions(List<Question> questions);
        Task<List<Question>> GetByProjectIdAsync(Guid id);
        Task SubstituteRangeAsync(List<Question> questions, Guid projectId);
    }
}
