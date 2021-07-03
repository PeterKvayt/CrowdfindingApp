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
