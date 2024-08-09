using BaseInsightDotNet.Commons.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Enumerate.Gender Gender { get; set; } = Enumerate.Gender.Unknown;
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
    }
}
