using Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Хакатон.Models;

[Route("api/[controller]")]
[ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(int? authorId, int? genreId)
    {
        IQueryable<Book> query = _context.Books;

        if (authorId != null)
        {
            query = query.Where(b => b.AuthorId == authorId);
        }

        if (genreId != null)
        {
            query = query.Where(b => b.GenreId == genreId);
        }

        return await query.ToListAsync();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        _context.Update(book);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
