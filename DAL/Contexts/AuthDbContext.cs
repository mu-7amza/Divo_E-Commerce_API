using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");


            var adminRoleId = "3d73c44b-9a12-4e6c-926e-0db03f4dc908";
            var userRoleId = "1c2b730a-226c-49a4-aa68-ce046cc98ca4";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var admin = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@local.com",
                NormalizedEmail = "admin@local.com".ToUpper(),
            };

            admin.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(admin, "Admin@123");

            builder.Entity<ApplicationUser>().HasData(admin);

            var userroles = new List<IdentityUserRole<string>>
            {
                new()
                {
                    UserId = admin.Id,
                    RoleId = adminRoleId
                },
                new()
                {
                    UserId = admin.Id,
                    RoleId = userRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(userroles);

        }
    }
}
