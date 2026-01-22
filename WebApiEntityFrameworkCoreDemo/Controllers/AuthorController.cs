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
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors(CancellationToken token, bool includeBooks = false)
        {
            var authors = await _authorService.GetAuthorsAsync(token, includeBooks);

            if (authors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
            }

            return StatusCode(StatusCodes.Status200OK, authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(Guid id, CancellationToken token, bool includeBooks = true)
        {
            Author author = await _authorService.GetAuthorAsync(id, token, includeBooks);

            if (author == null)
            {
                return  StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(AuthorRequest author, CancellationToken token)
        {
            var response = await _authorService.AddAuthorAsync(author, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            var dbAuthor = await _authorService.GetAuthorAsync(response.Id.Value, token, true);
            return CreatedAtAction("GetAuthor", new { id = response.Id }, dbAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, AuthorRequest author, CancellationToken token)
        {
            var response = await _authorService.UpdateAuthorAsync(id, author, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken token)
        {
            var response = await _authorService.DeleteAuthorAsync(id, token);

            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }

            return StatusCode(StatusCodes.Status200OK, response.Message);
        }
    }
}
