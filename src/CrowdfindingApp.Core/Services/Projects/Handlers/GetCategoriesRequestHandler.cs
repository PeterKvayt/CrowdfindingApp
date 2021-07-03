using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class GetCategoriesRequestHandler : NullOperationContextRequestHandler<CategoriesSearchRequestMessage, ReplyMessage<List<KeyValue<string, string>>>>
    {
        private readonly IProjectRepository _repository;

        public GetCategoriesRequestHandler(IProjectRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ReplyMessage<List<KeyValue<string, string>>>> ExecuteAsync(CategoriesSearchRequestMessage request)
        {
            var categories = await _repository.GetCategoriesAsync();

            return new ReplyMessage<List<KeyValue<string, string>>>
            {
                Value = categories.Select(x => new KeyValue<string, string>(x.Id.ToString(), x.Name)).ToList()
            };
        }
    }
}
