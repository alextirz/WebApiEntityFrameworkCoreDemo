using Microsoft.AspNetCore.Mvc;
using WebApiEntityFrameworkCoreDemo.DTOs;
using WebApiEntityFrameworkCoreDemo.Models;
using WebApiEntityFrameworkCoreDemo.Services;

namespace WebApiEntityFrameworkCoreDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public BookController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(CancellationToken token)
        {
            var books = await _libraryService.GetBooksAsync(token);
            if (books == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
            }

            return StatusCode(StatusCodes.Status200OK, books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooks(Guid id, CancellationToken token, bool includeAuthors = true)
        {
            Book book = await _libraryService.GetBookAsync(id, token, includeAuthors);

            if (book == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(BookRequest request, CancellationToken token)
        {
            var response = await _libraryService.AddBookAsync(request, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            var dbBook = await _libraryService.GetBookAsync(response.Id.Value, token, true);
            return CreatedAtAction("GetBook", new { id = response.Id }, dbBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookRequest request, CancellationToken token)
        {
            var response = await _libraryService.UpdateBookAsync(id, request, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken token)
        {
            var response = await _libraryService.DeleteBookAsync(id, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return StatusCode(StatusCodes.Status200OK, response.Message);
        }
    }
}
