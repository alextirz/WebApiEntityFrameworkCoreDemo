using WebApiEntityFrameworkCoreDemo.DTOs;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetBooksAsync(CancellationToken cancellationToken, bool includeAuthors = false);
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken, bool includeAuthors = false);
        Task<ApiResponse> AddBookAsync(BookRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> UpdateBookAsync(Guid id, BookRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> DeleteBookAsync(Guid id, CancellationToken cancellationToken);
    }
}
