using Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Хакатон.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly LibraryContext _context;

    public AuthorsController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return await _context.Authors.ToListAsync();
        // Метод для получения всех авторов
        // Возвращает список авторов в формате ActionResult, обернутый в Task
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return author;
    }

    [HttpPost]
    public async Task<ActionResult<Author>> AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }
}
