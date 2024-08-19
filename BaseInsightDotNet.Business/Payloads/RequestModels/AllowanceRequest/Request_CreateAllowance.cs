using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.AllowanceRequest
{
    public class Request_CreateAllowance
    {
        [Required(ErrorMessage = "Allowance name is required")]
        public string AllowanceName { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public double Amount { get; set; }
    }
}
