﻿using BaseInsightDotNet.Commons.Enums;
using BaseInsightDotNet.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest
{
    public class Request_UpdateContract
    {
        public Guid Id { get; set; }
        public string? EmployeeId { get; set; }
        public string? Content { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? BaseSalary { get; set; }
        public double? TaxPercentage { get; set; }
        public Guid? ContractTypeId { get; set; }
    }
}
