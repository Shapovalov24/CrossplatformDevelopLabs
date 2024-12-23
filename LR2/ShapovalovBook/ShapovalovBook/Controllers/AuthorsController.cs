using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShapovalovBook.Data;
using ShapovalovBook.Models;

namespace ShapovalovBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
            return Ok(authors);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            if (author == null)
                return BadRequest("Author cannot be null");

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // GET: api/authors/with-multiple-books
        [HttpGet("with-multiple-books")]
        public async Task<IActionResult> GetAuthorsWithMultipleBooks()
        {
            var authors = await _context.Authors
                .Where(a => a.Books.Count > 1)
                .Select(a => new
                {
                    a.Name,
                    BookCount = a.Books.Count
                })
                .ToListAsync();

            return Ok(authors);
        }
    }
}
