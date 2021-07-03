using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfindingApp.Common.Core.Messages.Users
{
    public class ForgotPasswordRequestMessage : MessageBase
    {
        public string Email { get; set; }
    }
}
