using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.SignalRModel
{
    public class ConnectionShare
    {
        public string Account { get; set; }
        public string ConnectionId { get; set; }
        public bool IsSharing { get; set; }
    }
}
