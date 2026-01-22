using WebApiEntityFrameworkCoreDemo.DTOs;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    // TODO: refactor this tp separate services
    public interface ILibraryService
    {
        Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken);
        Task<Author> GetAuthorAsync(Guid id, CancellationToken cancellationToken, bool includeBooks = false);
        Task<ErrorResponse> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken);
        Task<ErrorResponse> UpdateAuthorAsync(Guid id, AuthorRequest request, CancellationToken cancellationToken);
        Task<ErrorResponse> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken);
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken, bool includeAuthors = false);
        Task<ErrorResponse> AddBookAsync(BookRequest request, CancellationToken cancellationToken);
        Task<ErrorResponse> UpdateBookAsync(Guid id, BookRequest request, CancellationToken cancellationToken);
        Task<ErrorResponse> DeleteBookAsync(Guid id, CancellationToken cancellationToken);
    }
}
