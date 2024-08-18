using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract
{
    public class DataResponseContractType : DataResponseBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
