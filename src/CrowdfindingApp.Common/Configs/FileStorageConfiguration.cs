using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfindingApp.Common.Configs
{
    public class FileStorageConfiguration
    {
        public string Root { get; set; }
        public string TempFolderName { get; set; }
        public string PermanentFolderName { get; set; }
        public int TempStorageFileExpirationHours { get; set; }
    }
}
