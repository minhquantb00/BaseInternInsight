using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.DepartmentRequest
{
    public class Request_UpdateDepartment
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Slogan { get; set; }
        public string? ManagerId { get; set; }
    }
}
