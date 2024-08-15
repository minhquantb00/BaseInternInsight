using BaseInsightDotNet.Commons.Enums;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest
{
    public class Request_FilterUser
    {
        public string? KeyWord { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
    }
}
