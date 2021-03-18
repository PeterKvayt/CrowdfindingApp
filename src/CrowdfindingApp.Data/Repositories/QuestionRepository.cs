using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
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
    }
}
