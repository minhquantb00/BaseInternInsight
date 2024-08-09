﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set;}
    }
}
