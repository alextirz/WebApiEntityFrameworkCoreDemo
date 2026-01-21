using Microsoft.AspNetCore.Mvc;
using WebApiEntityFrameworkCoreDemo.Models;
using WebApiEntityFrameworkCoreDemo.Services;

namespace WebApiEntityFrameworkCoreDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public AuthorController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors(CancellationToken token)
        {
            var authors = await _libraryService.GetAuthorsAsync(token);

            if (authors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
            }

            return StatusCode(StatusCodes.Status200OK, authors);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAuthor(Guid id, CancellationToken token, bool includeBooks = true)
        {
            Author author = await _libraryService.GetAuthorAsync(id, token, includeBooks);

            if (author == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(Author author, CancellationToken token)
        {
            var dbAuthor = await _libraryService.AddAuthorAsync(author, token);

            if (dbAuthor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Name} could not be added.");
            }

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAuthor(Guid id, Author author, CancellationToken token)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            Author dbAuthor = await _libraryService.UpdateAuthorAsync(author, token);

            if (dbAuthor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Name} could not be updated");
            }

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken token)
        {
            var author = await _libraryService.GetAuthorAsync(id, token, false);
            (bool status, string message) = await _libraryService.DeleteAuthorAsync(author, token);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, author);
        }
    }
}
