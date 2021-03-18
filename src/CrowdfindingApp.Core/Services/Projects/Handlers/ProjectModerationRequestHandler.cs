using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers.Projects;
using CrowdfindingApp.Common.Handlers;
using CrowdfindingApp.Common.Messages;
using CrowdfindingApp.Common.Messages.Projects;
using CrowdfindingApp.Core.Services.Projects.Validators;
using CrowdfindingApp.Common.Extensions;
using AutoMapper;
using CrowdfindingApp.Data.Common.Interfaces;
using CrowdfindingApp.Data.Common.Interfaces.Repositories;

namespace CrowdfindingApp.Core.Services.Projects.Handlers
{
    public class ProjectModerationRequestHandler : NullOperationContextRequestHandler<ProjectModerationRequestMessage, ReplyMessage<ProjectInfo>>
    {
        private IMapper _mapper;
        private IProjectRepository _repository;

        public ProjectModerationRequestHandler(IMapper mapper, IProjectRepository repository)
        {
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
            _repository = repository ?? throw new NullReferenceException(nameof(repository));
        }

        protected override async Task<ReplyMessageBase> ValidateRequestMessageAsync(ProjectModerationRequestMessage requestMessage)
        {
            var reply = await base.ValidateRequestMessageAsync(requestMessage);
            var validator = new ProjectModerationValidator();
            var validationResult = await validator.ValidateAsync(requestMessage.Data);
            return await reply.MergeAsync(validationResult);
        }

        protected override async Task<ReplyMessage<ProjectInfo>> ExecuteAsync(ProjectModerationRequestMessage request)
        {

            return new ReplyMessage<ProjectInfo>
            {
                Value = _mapper.Map<ProjectInfo>(request.Data)
            };
        }
    }
}
