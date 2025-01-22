using CleanArchitecture.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet for each entity
        public DbSet<Country> Countries { get; set; }
        public DbSet<Country> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
        }

        // OnModelCreating for model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for each entity
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);         
        }
    }
}
