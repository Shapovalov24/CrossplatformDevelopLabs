using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShapovalovBook.Data;
using ShapovalovBook.Models;

namespace ShapovalovBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book cannot be null");

            try
            {
                book.Validate(); // Проверяем валидность данных
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            try
            {
                book.Title = updatedBook.Title;
                book.Year = updatedBook.Year;

                book.Validate(); // Проверяем валидность данных перед обновлением
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/books/by-author/{authorId}
        [HttpGet("by-author/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            var booksByAuthor = await _context.Books
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();

            return Ok(booksByAuthor);
        }
    }
}
