using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataAllowance
{
    public class DataResponseAllowance : DataResponseBase
    {
        public string AllowanceName { get; set; }
        public double Ammount { get; set; }    
    }
}
