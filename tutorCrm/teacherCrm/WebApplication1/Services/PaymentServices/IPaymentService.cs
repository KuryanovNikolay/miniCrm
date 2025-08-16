using WebApplication1.Dtos.PaymentDtos;

namespace WebApplication1.Services.PaymentServices;

/// <summary>
/// Интерфейс сервиса для работы с платежами.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Получает платеж по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>DTO объекта платежа.</returns>
    Task<PaymentDto> GetPaymentByIdAsync(Guid id);

    /// <summary>
    /// Получает список всех платежей в системе.
    /// </summary>
    /// <returns>Список DTO объектов платежей.</returns>
    Task<List<PaymentDto>> GetAllPaymentsAsync();

    /// <summary>
    /// Создает новый платеж в системе.
    /// </summary>
    /// <param name="paymentDto">DTO с данными для создания платежа.</param>
    /// <returns>DTO созданного платежа.</returns>
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto paymentDto);

    /// <summary>
    /// Обновляет существующий платеж.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого платежа.</param>
    /// <param name="paymentDto">DTO с обновленными данными платежа.</param>
    Task UpdatePaymentAsync(Guid id, UpdatePaymentDto paymentDto);

    /// <summary>
    /// Удаляет платеж по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого платежа.</param>
    Task DeletePaymentAsync(Guid id);

    /// <summary>
    /// Получает платежи по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список DTO платежей преподавателя.</returns>
    Task<List<PaymentDto>> GetPaymentsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Получает платежи по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список DTO платежей студента.</returns>
    Task<List<PaymentDto>> GetPaymentsByStudentIdAsync(Guid studentId);
}