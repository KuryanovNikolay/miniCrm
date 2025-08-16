using tutorCrm.Models;

namespace WebApplication1.Repositories.LessonRepositories;

/// <summary>
/// Интерфейс репозитория для работы с уроками.
/// </summary>
public interface ILessonRepository
{
    /// <summary>
    /// Получить урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>Найденный урок или null.</returns>
    Task<Lesson?> GetLessonByIdAsync(Guid id);

    /// <summary>
    /// Получить все уроки.
    /// </summary>
    /// <returns>Список всех уроков.</returns>
    Task<List<Lesson>> GetAllLessonsAsync();

    /// <summary>
    /// Создать новый урок.
    /// </summary>
    /// <param name="lesson">Данные урока.</param>
    /// <returns>Созданный урок.</returns>
    Task<Lesson> CreateLessonAsync(Lesson lesson);

    /// <summary>
    /// Обновить существующий урок.
    /// </summary>
    /// <param name="lesson">Данные урока для обновления.</param>
    Task UpdateLessonAsync(Lesson lesson);

    /// <summary>
    /// Проверить существование урока.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>True, если урок существует, иначе false.</returns>
    Task<bool> LessonExistsAsync(Guid id);

    /// <summary>
    /// Удалить урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    Task DeleteLessonAsync(Guid id);

    /// <summary>
    /// Получить уроки по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список уроков преподавателя.</returns>
    Task<List<Lesson>> GetLessonsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Получить уроки по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список уроков студента.</returns>
    Task<List<Lesson>> GetLessonsByStudentIdAsync(Guid studentId);
}