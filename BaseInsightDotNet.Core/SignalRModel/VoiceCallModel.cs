using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.SignalRModel
{
    public class VoiceCallModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public bool AutoAcceptCall { get; set; }
    }
}
