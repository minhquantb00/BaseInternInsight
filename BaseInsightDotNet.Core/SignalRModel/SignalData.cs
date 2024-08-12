using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.SignalRModel
{
    public class SignalData
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Sdp { get; set; }
        public string Type { get; set; }
        public SignalDataCandidate Candidate { get; set; }
        public SignalTransceiverRequest TransceiverRequest { get; set; }
        public bool? Renegotiate { get; set; }
    }
}
