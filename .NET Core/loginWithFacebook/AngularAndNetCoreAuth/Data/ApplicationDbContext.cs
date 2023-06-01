using AngularAndNetCoreAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace AngularAndNetCoreAuth.Data
{
    public class UsersDbContext : IdentityDbContext<ApplicationUser>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<AspNetUserProfile> AspNetUserProfile { get; set; } = null!;
        public virtual DbSet<AspNetRefreshToken> AspNetRefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AspNetRefreshToken>(entity =>
            {
                entity.ToTable("AspNetRefreshToken");
            });

            builder.Entity<ApplicationUser>(builder =>
            {
                builder.Property(e => e.UserId).ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            });
            base.OnModelCreating(builder);
        }

    }


}
