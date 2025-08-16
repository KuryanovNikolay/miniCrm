using tutorCrm.Models;

namespace WebApplication1.Repositories.SubjectRepositories;

/// <summary>
/// Интерфейс репозитория для работы с предметами.
/// </summary>
public interface ISubjectRepository
{
    /// <summary>
    /// Получить предмет по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>Найденный предмет или null.</returns>
    Task<Subject?> GetSubjectByIdAsync(Guid id);

    /// <summary>
    /// Получить все предметы.
    /// </summary>
    /// <returns>Список всех предметов.</returns>
    Task<List<Subject>> GetAllSubjectsAsync();

    /// <summary>
    /// Создать новый предмет.
    /// </summary>
    /// <param name="subject">Данные предмета.</param>
    /// <returns>Созданный предмет.</returns>
    Task<Subject> CreateSubjectAsync(Subject subject);

    /// <summary>
    /// Обновить существующий предмет.
    /// </summary>
    /// <param name="subject">Данные предмета для обновления.</param>
    Task UpdateSubjectAsync(Subject subject);

    /// <summary>
    /// Проверить существование предмета.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>True, если предмет существует, иначе false.</returns>
    Task<bool> SubjectExistsAsync(Guid id);

    /// <summary>
    /// Удалить предмет по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    Task DeleteSubjectAsync(Guid id);
}