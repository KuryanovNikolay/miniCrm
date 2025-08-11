using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Repositories.LessonRepositories;

namespace WebApplication1.Repositories.LessonRepositories;

public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _db;

    public LessonRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Lesson?> GetLessonByIdAsync(Guid id)
    {
        return await _db.Lessons
            .Include(l => l.Teacher)
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lesson>> GetAllLessonsAsync()
    {
        return await _db.Lessons
            .Include(l => l.Teacher)
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .ToListAsync();
    }

    public async Task<Lesson> CreateLessonAsync(Lesson lesson)
    {
        lesson.Status = "Scheduled"; // Default status
        await _db.Lessons.AddAsync(lesson);
        await _db.SaveChangesAsync();
        return lesson;
    }

    public async Task UpdateLessonAsync(Lesson lesson)
    {
        _db.Lessons.Update(lesson);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> LessonExistsAsync(Guid id)
    {
        return await _db.Lessons.AnyAsync(l => l.Id == id);
    }

    public async Task DeleteLessonAsync(Guid id)
    {
        var lesson = await GetLessonByIdAsync(id);
        if (lesson != null)
        {
            _db.Lessons.Remove(lesson);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<Lesson>> GetLessonsByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Lessons
            .Where(l => l.TeacherId == teacherId)
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .ToListAsync();
    }

    public async Task<List<Lesson>> GetLessonsByStudentIdAsync(Guid studentId)
    {
        return await _db.Lessons
            .Where(l => l.StudentId == studentId)
            .Include(l => l.Teacher)
            .Include(l => l.Subject)
            .ToListAsync();
    }
}