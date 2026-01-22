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
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(CancellationToken token, bool includeAuthors = false)
        {
            var books = await _bookService.GetBooksAsync(token, includeAuthors);
            if (books == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
            }

            return StatusCode(StatusCodes.Status200OK, books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id, CancellationToken token, bool includeAuthors = true)
        {
            Book book = await _bookService.GetBookAsync(id, token, includeAuthors);

            if (book == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(BookRequest request, CancellationToken token)
        {
            var response = await _bookService.AddBookAsync(request, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            var dbBook = await _bookService.GetBookAsync(response.Id.Value, token, true);
            return CreatedAtAction("GetBook", new { id = response.Id }, dbBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookRequest request, CancellationToken token)
        {
            var response = await _bookService.UpdateBookAsync(id, request, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken token)
        {
            var response = await _bookService.DeleteBookAsync(id, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return StatusCode(StatusCodes.Status200OK, response.Message);
        }
    }
}
