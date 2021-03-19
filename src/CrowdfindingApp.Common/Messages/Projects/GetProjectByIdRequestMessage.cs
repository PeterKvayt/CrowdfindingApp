﻿
namespace CrowdfindingApp.Common.Messages.Projects
{
    public class GetProjectByIdRequestMessage : MessageBase
    {
        public GetProjectByIdRequestMessage()
        {

        }

        public GetProjectByIdRequestMessage(string id) : base()
        {
            ProjectId = id;
        }

        public string ProjectId { get; set; }
    }
}
