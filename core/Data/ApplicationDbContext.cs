using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Geodata.Data;
using Tochka.Areas.Hr.Data;
using Tochka.Models;

namespace Tochka.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<City>()
                .HasIndex(city => city.LatinName)
                .IsUnique();
            builder.Entity<Vacancy>()
                .HasIndex(vacancy => vacancy.Ref)
                .IsUnique();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
    }
}
