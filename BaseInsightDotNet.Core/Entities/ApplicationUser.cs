using BaseInsightDotNet.Commons.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("User_tbl")]
    public class ApplicationUser : IdentityUser
    {
        public Enumerate.Gender Gender { get; set; } = Enumerate.Gender.Unknown;
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public Guid? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public Guid? PositionId { get; set; }
        public virtual Position? Position { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Contract>? Contracts { get; set; }
        public virtual ICollection<Payroll>? Payrolls { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
