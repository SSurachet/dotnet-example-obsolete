using Core.Helpers.DbContextHelper;
using Microsoft.EntityFrameworkCore;
using Service.Data.Models;

namespace Service.Data
{
    public class ServiceContext : DbContext
    {
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UpdateCompositeKeys();

            base.OnModelCreating(modelBuilder);
        }

    }
}