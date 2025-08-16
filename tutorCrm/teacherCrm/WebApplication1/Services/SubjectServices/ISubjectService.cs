using WebApplication1.Dtos.SubjectDtos;

namespace WebApplication1.Services.SubjectServices;

/// <summary>
/// Интерфейс сервиса для работы с учебными предметами.
/// </summary>
public interface ISubjectService
{
    /// <summary>
    /// Получает предмет по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>DTO объекта предмета.</returns>
    Task<SubjectDto> GetSubjectByIdAsync(Guid id);

    /// <summary>
    /// Получает список всех учебных предметов.
    /// </summary>
    /// <returns>Список DTO объектов предметов.</returns>
    Task<List<SubjectDto>> GetAllSubjectsAsync();

    /// <summary>
    /// Создает новый учебный предмет.
    /// </summary>
    /// <param name="subjectDto">DTO с данными для создания предмета.</param>
    /// <returns>DTO созданного предмета.</returns>
    Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto);

    /// <summary>
    /// Обновляет существующий учебный предмет.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого предмета.</param>
    /// <param name="subjectDto">DTO с обновленными данными предмета.</param>
    Task UpdateSubjectAsync(Guid id, UpdateSubjectDto subjectDto);

    /// <summary>
    /// Удаляет учебный предмет по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого предмета.</param>
    Task DeleteSubjectAsync(Guid id);
}