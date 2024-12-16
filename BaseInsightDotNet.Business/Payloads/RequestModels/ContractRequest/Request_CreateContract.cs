using BaseInsightDotNet.Commons.Enums;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest
{
    public class Request_CreateContract
    {
        [Required(ErrorMessage = "EmployeeId is required")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "BaseSalary is required")]
        public double BaseSalary { get; set; }
        [Required(ErrorMessage = "TaxPercentage is required")]
        public double TaxPercentage { get; set; }
        [Required(ErrorMessage = "ContractTypeId is required")]
        public Guid ContractTypeId { get; set; }
    }
}
