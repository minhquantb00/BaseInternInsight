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
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractAllowance> ContractAllowances { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Position> Positions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //SeedMedia(builder);
            //SeedRoles(builder);
            SeedAllowance(builder);
            SeedContractType(builder);
            SeedDepartment(builder);
            SeedPosition(builder);
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
        //    builder.Entity<ApplicationRole>().HasData
        //        (
        //            new ApplicationRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //            new ApplicationRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
        //        );
        //}

        //private static void SeedMedia(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MediaFolder>().HasData
        //        (
        //            new MediaFolder() { Id = Guid.NewGuid(), Name = "Public", IsPublic = true, CanDetectTracks = true, Deleted = false, FilesCount = 0 },
        //            new MediaFolder() { Id = Guid.NewGuid(), Name = "FilesUpload", IsPublic = true, CanDetectTracks = false, Deleted = false, FilesCount = 0 }
        //        );
        //}

        private static void SeedDepartment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData
            (
                new Department { Id = Guid.NewGuid(), CreateTime = DateTime.Now, ManagerId = "1240b4b9-798c-4b54-8b69-e68565dc6ba9", Name = "Dev", Slogan = "Hế lô", NumberOfMember = 0 }
            );
        }

        private static void SeedContractType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractType>().HasData
            (
                new ContractType { Id = Guid.NewGuid(), Name = "CTV", Description = "Hợp đồng cộng tác viên" },
                new ContractType { Id = Guid.NewGuid(), Name = "Thử việc", Description = "Hợp đồng thử việc"},
                new ContractType { Id = Guid.NewGuid(), Name = "Chính thức", Description = "Hợp đồng chính thức"}
            );
        }

        private static void SeedAllowance(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allowance>().HasData
                (
                    new Allowance() { Id = Guid.NewGuid(), AllowanceName = "Phụ cấp ăn trưa", Amount = 50},
                    new Allowance() { Id = Guid.NewGuid(), AllowanceName = "Phụ cấp đi lại", Amount = 100},
                    new Allowance() { Id = Guid.NewGuid(), AllowanceName = "Phụ cấp ăn tối", Amount = 60}
                );
        }

        private static void SeedPosition(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasData
                (
                    new Position() { Id = Guid.NewGuid(), Name = "Thuế thu nhập", SalaryCoefficient = (decimal) 0.1}
                );
        }
    }
}
