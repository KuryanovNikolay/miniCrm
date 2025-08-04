using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;
using WebApplication1.Repositories.HomeworkRepositories;

namespace WebApplication1.Repositories.HomeworkRepositories;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly ApplicationDbContext _db;

    public HomeworkRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Homework?> GetHomeworkByIdAsync(Guid id)
    {
        return await _db.Homeworks
            .Include(h => h.Lesson)
            .Include(h => h.Teacher)
            .Include(h => h.Student)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<List<Homework>> GetAllHomeworksAsync()
    {
        return await _db.Homeworks
            .Include(h => h.Lesson)
            .Include(h => h.Teacher)
            .Include(h => h.Student)
            .ToListAsync();
    }

    public async Task<Homework> CreateHomeworkAsync(Homework homework)
    {
        await _db.Homeworks.AddAsync(homework);
        await _db.SaveChangesAsync();
        return homework;
    }

    public async Task UpdateHomeworkAsync(Homework homework)
    {
        _db.Homeworks.Update(homework);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> HomeworkExistsAsync(Guid id)
    {
        return await _db.Homeworks.AnyAsync(h => h.Id == id);
    }

    public async Task DeleteHomeworkAsync(Guid id)
    {
        var homework = await GetHomeworkByIdAsync(id);
        if (homework != null)
        {
            _db.Homeworks.Remove(homework);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<Homework>> GetHomeworksByStudentIdAsync(Guid studentId)
    {
        return await _db.Homeworks
            .Where(h => h.StudentId == studentId)
            .Include(h => h.Lesson)
            .Include(h => h.Teacher)
            .ToListAsync();
    }

    public async Task<List<Homework>> GetHomeworksByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Homeworks
            .Where(h => h.TeacherId == teacherId)
            .Include(h => h.Lesson)
            .Include(h => h.Student)
            .ToListAsync();
    }
}