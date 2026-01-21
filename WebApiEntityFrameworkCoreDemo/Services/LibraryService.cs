using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameworkCoreDemo.Data;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<LibraryService> _logger;

        public LibraryService(AppDbContext db, ILogger<LibraryService> logger)
        {
            _db = db;
            _logger = logger;
        }

        #region Authors

        public async Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Authors.ToListAsync(cancellationToken);
        }

        public async Task<Author> GetAuthorAsync(Guid id, CancellationToken cancellationToken = default, bool includeBooks = false)
        {
            return includeBooks ? await _db.Authors.Include(b => b.Books).FirstOrDefaultAsync(i => i.Id == id) : await _db.Authors.FindAsync(id);
        }

        public async Task<ErrorResponse> AddAuthorAsync(Author author, CancellationToken cancellationToken = default)
        {
            try
            {
                await _db.Authors.AddAsync(author, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return ErrorResponse.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding author {AuthorName}", author.Name);
                return ErrorResponse.Fail(ex.Message);
            }
        }

        public async Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken = default)
        {
            _db.Entry(author).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancellationToken);

            return author;
        }

        public async Task<(bool, string)> DeleteAuthorAsync(Author author, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbAuthor = await _db.Authors.FindAsync(author.Id);

                if (dbAuthor == null)
                {
                    return (false, "Author could not be found");
                }

                _db.Authors.Remove(author);
                await _db.SaveChangesAsync(cancellationToken);

                return (true, "Author got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Authors

        #region Books

        public async Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Books.ToListAsync(cancellationToken);
        }

        public async Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken = default, bool includeAuthors = false)
        {
            return includeAuthors ? await _db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id, cancellationToken)
                : await _db.Books.FindAsync(id, cancellationToken);
        }

        public async Task<Book> AddBookAsync(Book book, CancellationToken cancellationToken = default)
        {
            await _db.Books.AddAsync(book, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return await _db.Books.FindAsync(book.Id, cancellationToken);
        }

        public async Task<Book> UpdateBookAsync(Book book, CancellationToken cancellationToken = default)
        {
            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancellationToken);
            return book;
        }

        public async Task<(bool, string)> DeleteBookAsync(Book book, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbBook = await _db.Books.FindAsync(book.Id);

                if (dbBook == null)
                {
                    return (false, "Book could not be found.");
                }

                _db.Books.Remove(book);
                await _db.SaveChangesAsync(cancellationToken);

                return (true, "Book got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Books
    }
}
