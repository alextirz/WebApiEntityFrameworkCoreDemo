using WebApiEntityFrameworkCoreDemo.DTOs;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    // TODO: refactor this tp separate services
    public interface ILibraryService
    {
        Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken, bool includeBooks = false);
        Task<Author> GetAuthorAsync(Guid id, CancellationToken cancellationToken, bool includeBooks = false);
        Task<ApiResponse> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> UpdateAuthorAsync(Guid id, AuthorRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken, bool includeAuthors = false);
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken, bool includeAuthors = false);
        Task<ApiResponse> AddBookAsync(BookRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> UpdateBookAsync(Guid id, BookRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> DeleteBookAsync(Guid id, CancellationToken cancellationToken);
    }
}
