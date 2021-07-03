using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.DataTransfers;
using CrowdfundingApp.Common.Core.Handlers;
using CrowdfundingApp.Common.Core.Messages;
using CrowdfundingApp.Common.Core.Messages.Projects;
using CrowdfundingApp.Common.Data.Interfaces.Repositories;

namespace CrowdfundingApp.Core.Services.Projects.Handlers
{
    public class GetCountriesRequestHandler : NullOperationContextRequestHandler<CountriesSearchRequestMessage, ReplyMessage<List<KeyValue<string, string>>>>
    {
        private readonly IProjectRepository _repository;

        public GetCountriesRequestHandler(IProjectRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ReplyMessage<List<KeyValue<string, string>>>> ExecuteAsync(CountriesSearchRequestMessage request)
        {
            var countries = await _repository.GetCountriesAsync();

            return new ReplyMessage<List<KeyValue<string, string>>>
            {
                Value = countries.Where(x => x.Id != Common.Immutable.Data.WholeWorldDelivery).Select(x => new KeyValue<string, string>(x.Id.ToString(), x.Name)).ToList()
            };
        }
    }
}
