using WebApplication1.Dtos.HomeworkDtos;

namespace WebApplication1.Services.HomeworkServices;

/// <summary>
/// Интерфейс сервиса для работы с домашними заданиями.
/// </summary>
public interface IHomeworkService
{
    /// <summary>
    /// Получает домашнее задание по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задания.</param>
    /// <returns>DTO домашнего задания.</returns>
    Task<HomeworkDto> GetHomeworkByIdAsync(Guid id);

    /// <summary>
    /// Получает все домашние задания.
    /// </summary>
    /// <returns>Список DTO домашних заданий.</returns>
    Task<List<HomeworkDto>> GetAllHomeworksAsync();

    /// <summary>
    /// Создает новое домашнее задание.
    /// </summary>
    /// <param name="homeworkDto">DTO для создания задания.</param>
    /// <returns>Созданное DTO задания.</returns>
    Task<HomeworkDto> CreateHomeworkAsync(CreateHomeworkDto homeworkDto);

    /// <summary>
    /// Обновляет существующее домашнее задание.
    /// </summary>
    /// <param name="id">Идентификатор задания.</param>
    /// <param name="homeworkDto">DTO с обновленными данными.</param>
    Task UpdateHomeworkAsync(Guid id, UpdateHomeworkDto homeworkDto);

    /// <summary>
    /// Удаляет домашнее задание.
    /// </summary>
    /// <param name="id">Идентификатор задания.</param>
    Task DeleteHomeworkAsync(Guid id);

    /// <summary>
    /// Получает задания по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список DTO заданий.</returns>
    Task<List<HomeworkDto>> GetHomeworksByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Получает задания по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список DTO заданий.</returns>
    Task<List<HomeworkDto>> GetHomeworksByTeacherIdAsync(Guid teacherId);
}