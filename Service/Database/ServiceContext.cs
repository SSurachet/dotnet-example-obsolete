using System;
using Core.Helpers.DbContextHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Data.Models;

namespace Service.Data
{
    public class ServiceContext : IdentityDbContext
    <User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UpdateCompositeKeys();

            base.OnModelCreating(modelBuilder);
        }

    }
}