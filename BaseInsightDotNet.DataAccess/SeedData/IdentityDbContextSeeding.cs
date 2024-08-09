using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseInsightDotNet.Commons.Constants.Constant;

namespace BaseInsightDotNet.DataAccess.SeedData
{
    public class IdentityDbContextSeeding
    {
        public static async Task SeedAsync(IdentityDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            dbContext.Database.Migrate();
            if (!dbContext.Users.Any())
            {
                var defaultUser = new ApplicationUser { UserName = "Admin", Email = "admin@admin.com" };
                await userManager.CreateAsync(defaultUser, "Abc@123");
            }
            if (!dbContext.Roles.Any(x => x.Name == Roles.Admin))
            {
                await dbContext.Roles.AddAsync(new ApplicationRole() { Name = Roles.Admin, NormalizedName = Roles.Admin });
                dbContext.SaveChanges();
            }
            if (!dbContext.Roles.Any(x => x.Name == Roles.User))
            {
                await dbContext.Roles.AddAsync(new ApplicationRole() { Name = Roles.User, NormalizedName = Roles.User });
                dbContext.SaveChanges();
            }
            if (!dbContext.UserRoles.Any())
            {
                var defaultUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == "Admin");
                await userManager.AddToRolesAsync(defaultUser, new List<string> { Roles.Admin });
                dbContext.SaveChanges();
            }

        }
    }
}
