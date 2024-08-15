using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.DepartmentRequest
{
    public class Request_CreateDepartment
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Slogan { get; set; }
        [Required(ErrorMessage = "Manager is required")]
        public string ManagerId { get; set; }
    }
}
