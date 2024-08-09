using BaseInsightDotNet.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseInsightDotNet.Core.Entities.Media;

namespace BaseInsightDotNet.DataAccess.Data
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>, IDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<ConfirmEmail> ConfirmEmail { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<MediaFolder> MediaFolders { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<MediaStorage> MediaStorages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //SeedRoles(builder);
            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
        public async Task<int> CommitChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        //private static void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData
        //        (
        //            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //            new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
        //        );
        //}
    }
}
