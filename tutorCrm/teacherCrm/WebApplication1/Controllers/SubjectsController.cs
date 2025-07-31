using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Subjects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
    {
        return await _context.Subjects.ToListAsync();
    }

    // GET: api/Subjects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetSubject(Guid id)
    {
        var subject = await _context.Subjects.FindAsync(id);

        if (subject == null)
        {
            return NotFound();
        }

        return subject;
    }

    // PUT: api/Subjects/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubject(Guid id, Subject subject)
    {
        if (id != subject.Id)
        {
            return BadRequest();
        }

        _context.Entry(subject).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubjectExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Subjects
    [HttpPost]
    public async Task<ActionResult<Subject>> PostSubject(Subject subject)
    {
        subject.Id = Guid.NewGuid();
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
    }

    // DELETE: api/Subjects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubjectExists(Guid id)
    {
        return _context.Subjects.Any(e => e.Id == id);
    }
}

