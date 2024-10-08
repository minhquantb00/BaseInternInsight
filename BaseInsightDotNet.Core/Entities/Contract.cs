﻿using BaseInsightDotNet.Commons.Base;
using BaseInsightDotNet.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("Contract_tbl")]
    public class Contract : BaseEntity
    {
        public string EmployeeId { get; set; }
        public virtual ApplicationUser? Employee {  get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double BaseSalary { get; set; }
        public double SalaryBeforeTax { get; set; }
        public Enumerate.ContractStatus ContractStatus { get; set; } = Enumerate.ContractStatus.DangCho;
        public double TaxPercentage { get; set; }
        public Guid ContractTypeId { get; set; }
        public bool? ReceiveAllowance { get; set; } = false;
        public virtual ContractType? ContractType { get; set; }
        public string? SignatureA { get; set; }
        public string? SignatureB { get; set; }
        public bool IsSubsidized { get; set; } = false;
        public virtual ICollection<ContractAllowance>? ContractAllowances { get; set; }
        public virtual ICollection<ContractHistory>? ContractHistories { get; set; }
        public virtual ICollection<ContractAppendix>? ContractAppendices { get; set; }
    }
}
