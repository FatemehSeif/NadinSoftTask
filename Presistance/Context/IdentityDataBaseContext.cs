﻿using Application.Contexts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Context
{
    public class IdentityDataBaseContext : IdentityDbContext<IdentityUser>, IIdentityDataBaseContext
    {
       
        public IdentityDataBaseContext(DbContextOptions<IdentityDataBaseContext> options) : base(options)
        {
        }
        public  DbSet<User> Users {  get; set; }
        public EntityEntry Entry([NotNull] object entity)
        {
            return base.Entry(entity);
        }


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
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
                entity.Property(e => e.Email).IsRequired(true);
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
