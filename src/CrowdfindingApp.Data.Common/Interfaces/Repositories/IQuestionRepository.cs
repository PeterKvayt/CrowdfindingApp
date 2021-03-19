using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;

namespace CrowdfindingApp.Data.Common.Interfaces.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task AddQuestions(List<Question> questions);
        Task<List<Question>> GetByProjectIdAsync(Guid id);
        Task SubstituteRangeAsync(List<Question> questions, Guid projectId);
    }
}
