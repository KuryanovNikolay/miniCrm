using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Repositories.LessonRepositories;

namespace WebApplication1.Repositories.LessonRepositories;

/// <summary>
/// Репозиторий для работы с уроками в базе данных.
/// </summary>
public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Конструктор репозитория уроков.
    /// </summary>
    /// <param name="db">Контекст базы данных.</param>
    public LessonRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Получить урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>Найденный урок или null.</returns>
    public async Task<Lesson?> GetLessonByIdAsync(Guid id)
    {
        return await _db.Lessons
            .Include(l => l.Teacher)
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    /// <summary>
    /// Получить все уроки.
    /// </summary>
    /// <returns>Список всех уроков.</returns>
    public async Task<List<Lesson>> GetAllLessonsAsync()
    {
        return await _db.Lessons
            .Include(l => l.Teacher)
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .ToListAsync();
    }

    /// <summary>
    /// Создать новый урок.
    /// </summary>
    /// <param name="lesson">Данные урока.</param>
    /// <returns>Созданный урок.</returns>
    public async Task<Lesson> CreateLessonAsync(Lesson lesson)
    {
        lesson.Status = "Scheduled"; // Default status
        await _db.Lessons.AddAsync(lesson);
        await _db.SaveChangesAsync();
        return lesson;
    }

    /// <summary>
    /// Обновить существующий урок.
    /// </summary>
    /// <param name="lesson">Данные урока для обновления.</param>
    public async Task UpdateLessonAsync(Lesson lesson)
    {
        _db.Lessons.Update(lesson);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Проверить существование урока.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>True, если урок существует, иначе false.</returns>
    public async Task<bool> LessonExistsAsync(Guid id)
    {
        return await _db.Lessons.AnyAsync(l => l.Id == id);
    }

    /// <summary>
    /// Удалить урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    public async Task DeleteLessonAsync(Guid id)
    {
        var lesson = await GetLessonByIdAsync(id);
        if (lesson != null)
        {
            _db.Lessons.Remove(lesson);
            await _db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Получить уроки по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список уроков преподавателя.</returns>
    public async Task<List<Lesson>> GetLessonsByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Lessons
            .Where(l => l.TeacherId == teacherId)
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .ToListAsync();
    }

    /// <summary>
    /// Получить уроки по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список уроков студента.</returns>
    public async Task<List<Lesson>> GetLessonsByStudentIdAsync(Guid studentId)
    {
        return await _db.Lessons
            .Where(l => l.StudentId == studentId)
            .Include(l => l.Teacher)
            .Include(l => l.Subject)
            .ToListAsync();
    }
}