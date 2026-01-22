using Microsoft.AspNetCore.Mvc;
using WebApiEntityFrameworkCoreDemo.DTOs;
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
        public async Task<ActionResult<Author>> AddAuthor(AuthorRequest author, CancellationToken token)
        {
            var response = await _libraryService.AddAuthorAsync(author, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            var dbAuthor = await _libraryService.GetAuthorAsync(response.Id.Value, token, true);
            return CreatedAtAction("GetAuthor", new { id = response.Id }, dbAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, AuthorRequest author, CancellationToken token)
        {
            var response = await _libraryService.UpdateAuthorAsync(id, author, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken token)
        {
            var response = await _libraryService.DeleteAuthorAsync(id, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return StatusCode(StatusCodes.Status200OK, response.Message);
        }
    }
}
