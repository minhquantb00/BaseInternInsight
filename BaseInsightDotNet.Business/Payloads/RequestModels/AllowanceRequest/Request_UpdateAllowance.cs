using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.AllowanceRequest
{
    public class Request_UpdateAllowance
    {
        public Guid Id { get; set; }
        public string AllowanceName { get; set; }
        public double Amount { get; set; }
    }
}
