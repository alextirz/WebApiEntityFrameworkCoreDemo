using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Books);

            // for demo data
            new DbInitializer(modelBuilder).Seed();
        }
    }
}

// migration part: 
// In Visual Studio, open NuGet Package Manager Console from
// Tools -> NuGet Package Manager -> Package Manager Console and enter the following command
// 1. add-migration Initial
// 2. update-database –verbose
