using WebApplication1.Dtos.SubscriptionsDtos;

namespace WebApplication1.Services.SubscriptionServices;

/// <summary>
/// Интерфейс сервиса для работы с подписками.
/// </summary>
public interface ISubscriptionService
{
    /// <summary>
    /// Получает подписку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подписки.</param>
    /// <returns>DTO подписки.</returns>
    Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id);

    /// <summary>
    /// Получает все подписки в системе.
    /// </summary>
    /// <returns>Коллекция DTO подписок.</returns>
    Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();

    /// <summary>
    /// Получает подписки по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Коллекция DTO подписок студента.</returns>
    Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Получает подписки по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Коллекция DTO подписок преподавателя.</returns>
    Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Создает новую подписку.
    /// </summary>
    /// <param name="createDto">DTO для создания подписки.</param>
    /// <returns>DTO созданной подписки.</returns>
    Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto createDto);

    /// <summary>
    /// Обновляет существующую подписку.
    /// </summary>
    /// <param name="id">Идентификатор подписки.</param>
    /// <param name="updateDto">DTO с обновленными данными.</param>
    /// <returns>DTO обновленной подписки.</returns>
    Task<SubscriptionDto> UpdateSubscriptionAsync(Guid id, UpdateSubscriptionDto updateDto);

    /// <summary>
    /// Удаляет подписку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подписки.</param>
    Task DeleteSubscriptionAsync(Guid id);
}
