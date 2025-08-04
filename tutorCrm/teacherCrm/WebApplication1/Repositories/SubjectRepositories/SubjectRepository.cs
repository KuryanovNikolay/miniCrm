using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;
using WebApplication1.Repositories.SubjectRepositories;

namespace WebApplication1.Repositories.SubjectRepositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _db;

    public SubjectRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Subject?> GetSubjectByIdAsync(Guid id)
    {
        return await _db.Subjects
            .Include(s => s.Lessons)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Subject>> GetAllSubjectsAsync()
    {
        return await _db.Subjects
            .Include(s => s.Lessons)
            .ToListAsync();
    }

    public async Task<Subject> CreateSubjectAsync(Subject subject)
    {
        await _db.Subjects.AddAsync(subject);
        await _db.SaveChangesAsync();
        return subject;
    }

    public async Task UpdateSubjectAsync(Subject subject)
    {
        _db.Subjects.Update(subject);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> SubjectExistsAsync(Guid id)
    {
        return await _db.Subjects.AnyAsync(s => s.Id == id);
    }

    public async Task DeleteSubjectAsync(Guid id)
    {
        var subject = await GetSubjectByIdAsync(id);
        if (subject != null)
        {
            _db.Subjects.Remove(subject);
            await _db.SaveChangesAsync();
        }
    }
}