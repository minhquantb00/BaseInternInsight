using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Commons.Enums;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract
{
    public class DataResponseContract : DataResponseBase
    {
        public DataResponseUser Employee { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double BaseSalary { get; set; }
        public double SalaryBeforeTax { get; set; }
        public string ContractStatus { get; set; }
        public double TaxPercentage { get; set; }
        public string ContractTypeName { get; set; }
        public string ReceiveAllowance { get; set; }
        public string? SignatureA { get; set; }
        public string? SignatureB { get; set; }
        public string IsSubsidized { get; set; }
    }
}
