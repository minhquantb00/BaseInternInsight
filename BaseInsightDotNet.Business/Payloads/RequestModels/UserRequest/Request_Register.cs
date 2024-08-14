using BaseInsightDotNet.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.UserRequest
{
    public class Request_Register
    {
        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
        [DataType(DataType.Upload)]
        public Enumerate.Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
    }
}
