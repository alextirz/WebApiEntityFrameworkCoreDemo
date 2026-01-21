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
        Task<Author> UpdateAuthorAsync(Guid id, AuthorRequest request, CancellationToken cancellationToken);
        Task<(bool, string)> DeleteAuthorAsync(Author author, CancellationToken cancellationToken);

        Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken);
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken, bool includeAuthors = false);
        Task<Book> AddBookAsync(BookRequest request, CancellationToken cancellationToken);
        Task<Book> UpdateBookAsync(Guid id, BookRequest request, CancellationToken cancellationToken);
        Task<(bool, string)> DeleteBookAsync(Book book, CancellationToken cancellationToken);
    }
}
