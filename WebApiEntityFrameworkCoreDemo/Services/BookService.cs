using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameworkCoreDemo.Data;
using WebApiEntityFrameworkCoreDemo.DTOs;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<BookService> _logger;

        public BookService(AppDbContext db, ILogger<BookService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken = default, bool includeAuthors = false)
        {
            return includeAuthors
                ? await _db.Books.Include(b => b.Authors).ToListAsync(cancellationToken)
                : await _db.Books.ToListAsync(cancellationToken);
        }

        public async Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken = default, bool includeAuthors = false)
        {
            return includeAuthors ? await _db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id, cancellationToken)
                : await _db.Books.FindAsync(id, cancellationToken);
        }

        public async Task<ApiResponse> AddBookAsync(BookRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Description,
                    Price = request.Price
                };

                if (request.AuthorIds.Any())
                {
                    var authors = await _db.Authors.Where(a => request.AuthorIds.Contains(a.Id)).ToListAsync(cancellationToken);
                    book.Authors = authors;
                }

                await _db.Books.AddAsync(book, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return ApiResponse.Ok(book.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding book {BookTitle}", request.Title);
                return ApiResponse.Fail(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateBookAsync(Guid id, BookRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var book = await _db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
                if (book == null) return ApiResponse.Fail($"Book with id {id} not found");

                book.Title = request.Title;
                book.Description = request.Description;
                book.Price = request.Price;

                if (request.AuthorIds != null)
                {
                    var authors = await _db.Authors.Where(a => request.AuthorIds.Contains(a.Id)).ToListAsync(cancellationToken);
                    book.Authors = authors;
                }

                await _db.SaveChangesAsync(cancellationToken);
                return ApiResponse.Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book {BookId}", id);
                return ApiResponse.Fail(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var book = await _db.Books.FindAsync(id);

                if (book == null)
                {
                    return ApiResponse.Fail($"Book with id {id} not found");
                }

                _db.Books.Remove(book);
                await _db.SaveChangesAsync(cancellationToken);

                return ApiResponse.Ok("Book deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book {BookId}", id);
                return ApiResponse.Fail(ex.Message);
            }
        }
    }
}
