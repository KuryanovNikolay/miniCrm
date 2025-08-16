using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Repositories.SubjectRepositories;

namespace WebApplication1.Repositories.SubjectRepositories;

/// <summary>
/// Репозиторий для работы с предметами в базе данных.
/// </summary>
public class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Конструктор репозитория предметов.
    /// </summary>
    /// <param name="db">Контекст базы данных.</param>
    public SubjectRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Получить предмет по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>Найденный предмет или null.</returns>
    public async Task<Subject?> GetSubjectByIdAsync(Guid id)
    {
        return await _db.Subjects
            .Include(s => s.Lessons)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// Получить все предметы.
    /// </summary>
    /// <returns>Список всех предметов.</returns>
    public async Task<List<Subject>> GetAllSubjectsAsync()
    {
        return await _db.Subjects
            .Include(s => s.Lessons)
            .ToListAsync();
    }

    /// <summary>
    /// Создать новый предмет.
    /// </summary>
    /// <param name="subject">Данные предмета.</param>
    /// <returns>Созданный предмет.</returns>
    public async Task<Subject> CreateSubjectAsync(Subject subject)
    {
        await _db.Subjects.AddAsync(subject);
        await _db.SaveChangesAsync();
        return subject;
    }

    /// <summary>
    /// Обновить существующий предмет.
    /// </summary>
    /// <param name="subject">Данные предмета для обновления.</param>
    public async Task UpdateSubjectAsync(Subject subject)
    {
        _db.Subjects.Update(subject);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Проверить существование предмета.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>True, если предмет существует, иначе false.</returns>
    public async Task<bool> SubjectExistsAsync(Guid id)
    {
        return await _db.Subjects.AnyAsync(s => s.Id == id);
    }

    /// <summary>
    /// Удалить предмет по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
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