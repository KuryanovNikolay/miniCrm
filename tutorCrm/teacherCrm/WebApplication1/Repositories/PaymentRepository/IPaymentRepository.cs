using tutorCrm.Models;

namespace WebApplication1.Repositories.PaymentRepositories;

/// <summary>
/// Интерфейс репозитория для работы с платежами.
/// </summary>
public interface IPaymentRepository
{
    /// <summary>
    /// Получить платеж по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>Найденный платеж или null.</returns>
    Task<Payment?> GetPaymentByIdAsync(Guid id);

    /// <summary>
    /// Получить все платежи.
    /// </summary>
    /// <returns>Список всех платежей.</returns>
    Task<List<Payment>> GetAllPaymentsAsync();

    /// <summary>
    /// Создать новый платеж.
    /// </summary>
    /// <param name="payment">Данные платежа.</param>
    /// <returns>Созданный платеж.</returns>
    Task<Payment> CreatePaymentAsync(Payment payment);

    /// <summary>
    /// Обновить существующий платеж.
    /// </summary>
    /// <param name="payment">Данные платежа для обновления.</param>
    Task UpdatePaymentAsync(Payment payment);

    /// <summary>
    /// Проверить существование платежа.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>True, если платеж существует, иначе false.</returns>
    Task<bool> PaymentExistsAsync(Guid id);

    /// <summary>
    /// Удалить платеж по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    Task DeletePaymentAsync(Guid id);

    /// <summary>
    /// Получить платежи по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список платежей преподавателя.</returns>
    Task<List<Payment>> GetPaymentsByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Получить платежи по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список платежей студента.</returns>
    Task<List<Payment>> GetPaymentsByStudentIdAsync(Guid studentId);
}