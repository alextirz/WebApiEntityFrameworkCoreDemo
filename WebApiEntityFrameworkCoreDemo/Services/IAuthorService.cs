using WebApiEntityFrameworkCoreDemo.DTOs;
using WebApiEntityFrameworkCoreDemo.Models;

namespace WebApiEntityFrameworkCoreDemo.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthorsAsync(CancellationToken cancellationToken, bool includeBooks = false);
        Task<Author> GetAuthorAsync(Guid id, CancellationToken cancellationToken, bool includeBooks = false);
        Task<ApiResponse> AddAuthorAsync(AuthorRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> UpdateAuthorAsync(Guid id, AuthorRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken);
    }
}
