using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser
{
    public class DataResponseUser 
    {
        public string Id { get; set; }
        public string Gender { get; set; }  
        public DateTime DateOfBirth { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
    }
}
