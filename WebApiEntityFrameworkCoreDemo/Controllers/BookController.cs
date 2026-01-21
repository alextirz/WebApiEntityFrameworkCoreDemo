using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetBooks(Guid id, CancellationToken token)
        {
            Book book = await _libraryService.GetBookAsync(id, token);

            if (book == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book, CancellationToken token)
        {
            var dbBook = await _libraryService.AddBookAsync(book, token);

            if (dbBook == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{book.Title} could not be added.");
            }

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, Book book, CancellationToken token)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            Book dbBook = await _libraryService.UpdateBookAsync(book, token);

            if (dbBook == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{book.Title} could not be updated");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken token)
        {
            var book = await _libraryService.GetBookAsync(id, token);
            (bool status, string message) = await _libraryService.DeleteBookAsync(book, token);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, book);
        }
    }
}
