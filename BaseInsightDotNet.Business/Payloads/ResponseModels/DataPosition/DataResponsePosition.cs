using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataPosition
{
    public class DataResponsePosition : DataResponseBase
    {
        public string Name { get; set; }
        public decimal SalaryCoefficient { get; set; }
    }
}
