using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels
{
    public class DataResponseLoginOTP
    {
        public string Token { get; set; }
        public bool IsTwoFactorEnable { get; set; }
    }
}
