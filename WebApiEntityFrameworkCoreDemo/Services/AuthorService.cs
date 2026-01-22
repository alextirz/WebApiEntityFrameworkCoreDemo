using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameworkCoreDemo.Data;
using WebApiEntityFrameworkCoreDemo.DTOs;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(AppDbContext db, ILogger<AuthorService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken = default, bool includeBooks = false)
        {
            return includeBooks
                ? await _db.Authors.Include(a => a.Books).ToListAsync(cancellationToken)
                : await _db.Authors.ToListAsync(cancellationToken);
        }

        public async Task<Author> GetAuthorAsync(Guid id, CancellationToken cancellationToken = default, bool includeBooks = false)
        {
            return includeBooks ? await _db.Authors.Include(b => b.Books).FirstOrDefaultAsync(i => i.Id == id) : await _db.Authors.FindAsync(id);
        }

        public async Task<ApiResponse> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    BirthDate = request.BirthDate
                };

                if (request.BookIds.Any())
                {
                    var books = await _db.Books.Where(b => request.BookIds.Contains(b.Id)).ToListAsync(cancellationToken);
                    author.Books = books;
                }

                await _db.Authors.AddAsync(author, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return ApiResponse.Ok(author.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding author {AuthorName}", request.Name);
                return ApiResponse.Fail(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAuthorAsync(Guid id, AuthorRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var author = await _db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
                if (author == null) return ApiResponse.Fail($"Author with id {id} not found");

                author.Name = request.Name;
                author.BirthDate = request.BirthDate;

                if (request.BookIds != null)
                {
                    var books = await _db.Books.Where(b => request.BookIds.Contains(b.Id)).ToListAsync(cancellationToken);
                    author.Books = books;
                }

                await _db.SaveChangesAsync(cancellationToken);
                return ApiResponse.Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating author {AuthorId}", id);
                return ApiResponse.Fail(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var author = await _db.Authors.FindAsync(id);

                if (author == null)
                {
                    return ApiResponse.Fail($"Author with id {id} not found");
                }

                _db.Authors.Remove(author);
                await _db.SaveChangesAsync(cancellationToken);

                return ApiResponse.Ok("Author deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting author {AuthorId}", id);
                return ApiResponse.Fail(ex.Message);
            }
        }
    }
}
