using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.DBContext
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        public DbSet<AuthUser> AuthUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthUser>()
                .HasIndex(u => u.Username)
                .IsUnique(); // Make Username unique
        }
    }
}
