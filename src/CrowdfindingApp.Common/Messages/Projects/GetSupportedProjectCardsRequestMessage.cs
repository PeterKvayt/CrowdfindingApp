using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdfindingApp.Common.DataTransfers;

namespace CrowdfindingApp.Common.Messages.Projects
{
    public class GetSupportedProjectCardsRequestMessage : MessageBase
    {
        public PagingInfo Paging { get; set; }
    }
}
