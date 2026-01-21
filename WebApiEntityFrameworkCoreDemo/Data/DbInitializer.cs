using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var firstAuthorId = Guid.NewGuid();
            var secondAuthorId = Guid.NewGuid();
            var firstBookId = Guid.NewGuid();
            var secondBookId = Guid.NewGuid();
            var thirdBookId = Guid.NewGuid();

            _modelBuilder.Entity<Author>(x =>
            {
                x.HasData(new Author
                {
                    Id = firstAuthorId,
                    Name = "Vasya P",
                    BirthDate = new DateTime(1900, 01, 01)
                });
                x.HasData(new Author
                {
                    Id = secondAuthorId,
                    Name = "Petya R",
                    BirthDate = new DateTime(1910, 02, 01)
                });
            });

            _modelBuilder.Entity<Book>(x =>
            {
                x.HasData(new Book
                {
                    Id = firstBookId,
                    Title = "Title1",
                    Description = "Description1",
                    Price = 123.11m
                });
                x.HasData(new Book
                {
                    Id = secondBookId,
                    Title = "Title2",
                    Description = "Description2",
                    Price = 113.11m
                });
                x.HasData(new Book
                {
                    Id = thirdBookId,
                    Title = "Title3",
                    Description = "Description3",
                    Price = 1523.11m
                });
            });

            _modelBuilder.Entity("AuthorBook").HasData(
                new { AuthorsId = firstAuthorId, BooksId = firstBookId },
                new { AuthorsId = firstAuthorId, BooksId = secondBookId },
                new { AuthorsId = secondAuthorId, BooksId = thirdBookId },
                new { AuthorsId = secondAuthorId, BooksId = firstBookId } 
            );
        }
    }
}
