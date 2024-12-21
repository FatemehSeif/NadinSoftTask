using Application;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Context
{
    public class IdentityDataBaseContext : IdentityDbContext   , IIdentityDataBaseContext  
    {
        public IdentityDataBaseContext(DbContextOptions<IdentityDataBaseContext> options) : base(options)
        {

        }
        DbSet<User> Users {  get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser<string>>().ToTable("Users", "identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "identity");
            builder.Entity<IdentityRole<string>>().ToTable("Roles", "identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "identity");

            builder.Entity<IdentityUser<string>>(entity => {
                entity.Property(e => e.Email).IsRequired(false);
            });

            builder.Entity<IdentityUserLogin<string>>().HasKey(p => new
            {
                p.LoginProvider,
                p.ProviderKey
            });
            builder.Entity<IdentityUserRole<string>>().HasKey(p => new
            {
                p.UserId,
                p.RoleId
            });
            builder.Entity<IdentityUserToken<string>>().HasKey(p => new
            {
                p.UserId,
                p.LoginProvider,
                p.Name
            });
        }
    }
}
