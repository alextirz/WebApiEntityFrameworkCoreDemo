using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    // TODO: refactor this
    public interface ILibraryService
    {
        Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken);
        Task<Author> GetAuthorAsync(Guid id, CancellationToken cancellationToken, bool includeBooks = false);
        Task<Author> AddAuthorAsync(Author author, CancellationToken cancellationToken);
        Task<Author> UpdateAuthorAsync(Author author, CancellationToken cancellationToken);
        Task<(bool, string)> DeleteAuthorAsync(Author author, CancellationToken cancellationToken);

        Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken);
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken);
        Task<Book> AddBookAsync(Book book, CancellationToken cancellationToken);
        Task<Book> UpdateBookAsync(Book book, CancellationToken cancellationToken);
        Task<(bool, string)> DeleteBookAsync(Book book, CancellationToken cancellationToken);
    }
}
