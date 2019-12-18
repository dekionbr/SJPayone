using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJPayone.Models
{
    public class PayOneSettings
    {
        public string AId { get; set; }
        public string MId { get; set; }
        public string PortalId { get; set; }
        public string Key { get; set; }
        public string Mode { get; set; }
        public string Api_Version { get; set; }
        public string Encoding { get; set; }
    }
}
