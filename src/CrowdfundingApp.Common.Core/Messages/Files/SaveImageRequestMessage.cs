﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace CrowdfundingApp.Common.Core.Messages.Files
{
    public class SaveImageRequestMessage : SaveFileRequestMessageBase
    {
        public SaveImageRequestMessage()
        {

        }

        public SaveImageRequestMessage(IFormFile file)
        {
            File = file;
        }
    }
}
