using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Entities;

namespace MyWebApplication
{
    public class SchoolContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Person> People { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed People Table
            _ = builder.Entity<Person>().HasData(new Person { Id = 1, FullName = "Marlon Brando", Age = 40 });
            _ = builder.Entity<Person>().HasData(new Person { Id = 2, FullName = "John Doe", Age = 124 });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder.UseSqlServer("TestConnection");
            }
        }

        internal async Task<Person> GetFirstPersonAsync()
        {
            return await People.FirstAsync();
        }
    }
}
