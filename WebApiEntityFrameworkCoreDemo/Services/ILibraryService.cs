using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    // TODO: refactor this tp separate services
    public interface ILibraryService
    {
        Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken);
        Task<Author> GetAuthorAsync(Guid id, CancellationToken cancellationToken, bool includeBooks = false);
        Task<ErrorResponse> AddAuthorAsync(Author author, CancellationToken cancellationToken);
        Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken);
        Task<(bool, string)> DeleteAuthorAsync(Author author, CancellationToken cancellationToken);

        Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken);
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken, bool includeAuthors = false);
        Task<Book> AddBookAsync(Book book, CancellationToken cancellationToken);
        Task<Book> UpdateBookAsync(Book book, CancellationToken cancellationToken);
        Task<(bool, string)> DeleteBookAsync(Book book, CancellationToken cancellationToken);
    }
}
