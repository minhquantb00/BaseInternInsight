using BaseInsightDotNet.Commons.Enums;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest
{
    public class Request_CreateContract
    {
        public string EmployeeId { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double BaseSalary { get; set; }
        public double SalaryBeforeTax { get; set; }
        public double TaxPercentage { get; set; }
        public Guid ContractTypeId { get; set; }
    }
}
