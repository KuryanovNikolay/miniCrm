using tutorCrm.Models;

namespace WebApplication1.Repositories.HomeworkRepositories;

/// <summary>
/// Интерфейс репозитория для работы с домашними заданиями.
/// </summary>
public interface IHomeworkRepository
{
    /// <summary>
    /// Получает домашнее задание по идентификатору.
    /// </summary>
    Task<Homework?> GetHomeworkByIdAsync(Guid id);

    /// <summary>
    /// Получает список всех домашних заданий.
    /// </summary>
    Task<List<Homework>> GetAllHomeworksAsync();

    /// <summary>
    /// Создает новое домашнее задание.
    /// </summary>
    Task<Homework> CreateHomeworkAsync(Homework homework);

    /// <summary>
    /// Обновляет существующее домашнее задание.
    /// </summary>
    Task UpdateHomeworkAsync(Homework homework);

    /// <summary>
    /// Проверяет, существует ли домашнее задание с заданным идентификатором.
    /// </summary>
    Task<bool> HomeworkExistsAsync(Guid id);

    /// <summary>
    /// Удаляет домашнее задание по идентификатору.
    /// </summary>
    Task DeleteHomeworkAsync(Guid id);

    /// <summary>
    /// Получает все домашние задания студента.
    /// </summary>
    Task<List<Homework>> GetHomeworksByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Получает все домашние задания преподавателя.
    /// </summary>
    Task<List<Homework>> GetHomeworksByTeacherIdAsync(Guid teacherId);
}
