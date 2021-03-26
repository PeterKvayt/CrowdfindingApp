using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Extensions;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Data.Common.Filters;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;
using CrowdfindingApp.Data.Common.Models;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class ProjectSearchRequestHandler : NullOperationContextRequestHandler<ProjectSearchRequestMessage, ReplyMessage<List<ProjectInfo>>>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _repository;

        public ProjectSearchRequestHandler(IMapper mapper, IProjectRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(ProjectSearchRequestMessage request)
        {
            var reply = await base.ValidateRequestMessageAsync(request);

            if((request.Filter?.Id?.AnyNonEmptyOrWhitespace() ?? false) 
                && request.Filter.Id.Any(x => !Guid.TryParse(x, out Guid _)))
            {
                var invalidId = request.Filter.Id.First(x => !Guid.TryParse(x, out Guid _));
                return reply.AddValidationError(CommonErrorMessageKeys.InvalidIdFormat, parameters: invalidId);
            }

            if((request.Filter?.CategoryId?.AnyNonEmptyOrWhitespace() ?? false)
                && request.Filter.CategoryId.Any(x => !Guid.TryParse(x, out Guid _)))
            {
                var invalidId = request.Filter.CategoryId.First(x => !Guid.TryParse(x, out Guid _));
                return reply.AddValidationError(CommonErrorMessageKeys.InvalidCategoryId, parameters: invalidId);
            }

            return reply;
        }

        protected override async Task<ReplyMessage<List<ProjectInfo>>> ExecuteAsync(ProjectSearchRequestMessage request)
        {
            var filter = new ProjectFilter { OwnerId = new List<Guid> { User.GetUserId() } };
            _mapper.Map(request.Filter, filter);
            var paging = _mapper.Map<Paging>(request.Paging);
            var projects = await _repository.GetProjectsAsync(filter, paging);

            return new ReplyMessage<List<ProjectInfo>> { Value = projects.Select(_mapper.Map<ProjectInfo>).ToList() };
        }
    }
}
