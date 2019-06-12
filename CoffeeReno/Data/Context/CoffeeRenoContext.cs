using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class CoffeeRenoContext : DbContext
    {
        public CoffeeRenoContext(DbContextOptions<CoffeeRenoContext> options)
            : base(options)
        { }

        public DbSet<UserLoginHistory> UserLoginHistorys { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLoginHistory>().ToTable("UserLoginHistory");
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Role>().ToTable("Role");
        }
    }
}
