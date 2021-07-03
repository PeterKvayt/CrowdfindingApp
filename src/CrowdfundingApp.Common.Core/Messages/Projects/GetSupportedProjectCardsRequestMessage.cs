using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdfundingApp.Common.Core.DataTransfers;

namespace CrowdfundingApp.Common.Core.Messages.Projects
{
    public class GetSupportedProjectCardsRequestMessage : MessageBase
    {
        public PagingInfo Paging { get; set; }
    }
}
