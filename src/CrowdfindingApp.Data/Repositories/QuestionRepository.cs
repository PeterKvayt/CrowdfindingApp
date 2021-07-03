using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Interfaces;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        protected override DbSet<Question> Repository => Storage.Questions;

        public QuestionRepository(IDataProvider storage) : base(storage)
        {

        }

        public async Task AddQuestions(List<Question> questions)
        {
            foreach(var q in questions)
            {
                q.Id = new Guid();
            }
            await Repository.AddRangeAsync(questions);
            await Storage.SaveChangesAsync();
        }

        public async Task<List<Question>> GetByProjectIdAsync(Guid id)
        {
            return await GetQuery().Where(x => x.ProjectId == id).ToListAsync();
        }

        public async Task SubstituteRangeAsync(List<Question> questions, Guid projectId)
        {
            var questionsToSubstitute = await GetQuery()
                .Where(x => x.ProjectId == projectId && !questions.Select(_ => _.Id).Contains(x.Id))
                .ToListAsync();
            if(questionsToSubstitute?.Any() ?? false) Repository.RemoveRange(questionsToSubstitute);

            Repository.UpdateRange(questions);
            await Storage.SaveChangesAsync();
        }
    }
}
