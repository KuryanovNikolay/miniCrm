using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Repositories.HomeworkRepositories;

namespace WebApplication1.Repositories.HomeworkRepositories;

/// <summary>
/// Репозиторий для работы с домашними заданиями.
/// </summary>
public class HomeworkRepository : IHomeworkRepository
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="HomeworkRepository"/>.
    /// </summary>
    /// <param name="db">Контекст базы данных.</param>
    public HomeworkRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Получает домашнее задание по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор домашнего задания.</param>
    /// <returns>Домашнее задание или null, если не найдено.</returns>
    public async Task<Homework?> GetHomeworkByIdAsync(Guid id)
    {
        return await _db.Homeworks
            .Include(h => h.Lesson)
            .Include(h => h.Teacher)
            .Include(h => h.Student)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    /// <summary>
    /// Получает список всех домашних заданий.
    /// </summary>
    /// <returns>Список домашних заданий.</returns>
    public async Task<List<Homework>> GetAllHomeworksAsync()
    {
        return await _db.Homeworks
            .Include(h => h.Lesson)
            .Include(h => h.Teacher)
            .Include(h => h.Student)
            .ToListAsync();
    }

    /// <summary>
    /// Создает новое домашнее задание.
    /// </summary>
    /// <param name="homework">Домашнее задание для создания.</param>
    /// <returns>Созданное домашнее задание.</returns>
    public async Task<Homework> CreateHomeworkAsync(Homework homework)
    {
        await _db.Homeworks.AddAsync(homework);
        await _db.SaveChangesAsync();
        return homework;
    }

    /// <summary>
    /// Обновляет существующее домашнее задание.
    /// </summary>
    /// <param name="homework">Домашнее задание для обновления.</param>
    public async Task UpdateHomeworkAsync(Homework homework)
    {
        _db.Homeworks.Update(homework);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Проверяет, существует ли домашнее задание с заданным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор домашнего задания.</param>
    /// <returns>True, если существует; иначе false.</returns>
    public async Task<bool> HomeworkExistsAsync(Guid id)
    {
        return await _db.Homeworks.AnyAsync(h => h.Id == id);
    }

    /// <summary>
    /// Удаляет домашнее задание по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор домашнего задания.</param>
    public async Task DeleteHomeworkAsync(Guid id)
    {
        var homework = await GetHomeworkByIdAsync(id);
        if (homework != null)
        {
            _db.Homeworks.Remove(homework);
            await _db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Получает все домашние задания студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список домашних заданий студента.</returns>
    public async Task<List<Homework>> GetHomeworksByStudentIdAsync(Guid studentId)
    {
        return await _db.Homeworks
            .Where(h => h.StudentId == studentId)
            .Include(h => h.Lesson)
            .Include(h => h.Teacher)
            .ToListAsync();
    }

    /// <summary>
    /// Получает все домашние задания преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список домашних заданий преподавателя.</returns>
    public async Task<List<Homework>> GetHomeworksByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Homeworks
            .Where(h => h.TeacherId == teacherId)
            .Include(h => h.Lesson)
            .Include(h => h.Student)
            .ToListAsync();
    }
}
