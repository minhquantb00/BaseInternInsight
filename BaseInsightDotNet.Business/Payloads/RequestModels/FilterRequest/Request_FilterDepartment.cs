using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest
{
    public class Request_FilterDepartment
    {
        public string? Name { get; set; }
        public string? ManagerId { get; set; }
    }
}
