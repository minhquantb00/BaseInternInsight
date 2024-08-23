using BaseInsightDotNet.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest
{
    public class Request_FilterContract
    {
        public Guid? ContractTypeId { get; set; }
        public string? EmployeeId { get; set; }
        public Enumerate.ContractStatus? ContractStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
