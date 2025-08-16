using WebApplication1.Dtos.LessonDtos;

namespace WebApplication1.Services.LessonServices;

/// <summary>
/// Интерфейс сервиса для работы с уроками.
/// </summary>
public interface ILessonService
{
    /// <summary>
    /// Получает урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>DTO объекта урока.</returns>
    Task<LessonDto> GetLessonByIdAsync(Guid id);

    /// <summary>
    /// Получает список всех уроков.
    /// </summary>
    /// <returns>Список DTO объектов уроков.</returns>
    Task<List<LessonDto>> GetAllLessonsAsync();

    /// <summary>
    /// Создает новый урок.
    /// </summary>
    /// <param name="lessonDto">DTO для создания урока.</param>
    /// <returns>DTO созданного урока.</returns>
    Task<LessonDto> CreateLessonAsync(CreateLessonDto lessonDto);

    /// <summary>
    /// Обновляет существующий урок.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <param name="lessonDto">DTO с обновленными данными урока.</param>
    Task UpdateLessonAsync(Guid id, UpdateLessonDto lessonDto);

    /// <summary>
    /// Удаляет урок по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    Task DeleteLessonAsync(Guid id);

    /// <summary>
    /// Получает уроки по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список DTO уроков преподавателя.</returns>
    Task<List<LessonDto>> GetLessonsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Получает уроки по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список DTO уроков студента.</returns>
    Task<List<LessonDto>> GetLessonsByStudentIdAsync(Guid studentId);
}