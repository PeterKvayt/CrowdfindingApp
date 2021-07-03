using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Core.DataTransfers;
using CrowdfindingApp.Common.Core.Handlers;
using CrowdfindingApp.Common.Core.Messages;
using CrowdfindingApp.Common.Core.Messages.Projects;
using CrowdfindingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class GetCitiesRequestHandler : NullOperationContextRequestHandler<CitiesSearchRequestMessage, ReplyMessage<List<KeyValue<string, string>>>>
    {
        private readonly IProjectRepository _repository;

        public GetCitiesRequestHandler(IProjectRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ReplyMessage<List<KeyValue<string, string>>>> ExecuteAsync(CitiesSearchRequestMessage request)
        {
            var cities = await _repository.GetCitiesAsync();

            return new ReplyMessage<List<KeyValue<string, string>>>
            {
                Value = cities.Select(x => new KeyValue<string, string>(x.Id.ToString(), x.Name)).ToList()
            };
        }
    }
}
