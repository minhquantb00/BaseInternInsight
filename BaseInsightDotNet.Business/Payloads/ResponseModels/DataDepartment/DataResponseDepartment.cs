using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataDepartment
{
    public class DataResponseDepartment : DataResponseBase
    {
        public string Name { get; set; }
        public string Slogan { get; set; }
        public int NumberOfMember { get; set; }
        public DataResponseUser Manager { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
