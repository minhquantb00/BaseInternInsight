using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels
{
    public class DataResponseLogin
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
