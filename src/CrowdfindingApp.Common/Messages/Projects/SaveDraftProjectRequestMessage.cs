﻿using CrowdfindingApp.Common.DataTransfers.Project;

namespace CrowdfindingApp.Common.Messages.Projects
{
    public class SaveDraftProjectRequestMessage : MessageBase
    {
        public DraftProjectInfo Data { get; set; }
    }
}
