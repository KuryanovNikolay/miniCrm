using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Repositories.PaymentRepositories;

namespace WebApplication1.Repositories.PaymentRepositories;

/// <summary>
/// Репозиторий для работы с платежами в базе данных.
/// </summary>
public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Конструктор репозитория платежей.
    /// </summary>
    /// <param name="db">Контекст базы данных.</param>
    public PaymentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Получить платеж по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>Найденный платеж или null.</returns>
    public async Task<Payment?> GetPaymentByIdAsync(Guid id)
    {
        return await _db.Payments
            .Include(p => p.Teacher)
            .Include(p => p.Student)
            .Include(p => p.Lesson)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Получить все платежи.
    /// </summary>
    /// <returns>Список всех платежей.</returns>
    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _db.Payments
            .Include(p => p.Teacher)
            .Include(p => p.Student)
            .Include(p => p.Lesson)
            .ToListAsync();
    }

    /// <summary>
    /// Создать новый платеж.
    /// </summary>
    /// <param name="payment">Данные платежа.</param>
    /// <returns>Созданный платеж.</returns>
    public async Task<Payment> CreatePaymentAsync(Payment payment)
    {
        payment.Status = "Pending"; // Default status
        payment.PaymentDate = DateTime.UtcNow;
        await _db.Payments.AddAsync(payment);
        await _db.SaveChangesAsync();
        return payment;
    }

    /// <summary>
    /// Обновить существующий платеж.
    /// </summary>
    /// <param name="payment">Данные платежа для обновления.</param>
    public async Task UpdatePaymentAsync(Payment payment)
    {
        _db.Payments.Update(payment);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Проверить существование платежа.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>True, если платеж существует, иначе false.</returns>
    public async Task<bool> PaymentExistsAsync(Guid id)
    {
        return await _db.Payments.AnyAsync(p => p.Id == id);
    }

    /// <summary>
    /// Удалить платеж по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    public async Task DeletePaymentAsync(Guid id)
    {
        var payment = await GetPaymentByIdAsync(id);
        if (payment != null)
        {
            _db.Payments.Remove(payment);
            await _db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Получить платежи по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список платежей преподавателя.</returns>
    public async Task<List<Payment>> GetPaymentsByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Payments
            .Where(p => p.TeacherId == teacherId)
            .Include(p => p.Student)
            .Include(p => p.Lesson)
            .ToListAsync();
    }

    /// <summary>
    /// Получить платежи по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список платежей студента.</returns>
    public async Task<List<Payment>> GetPaymentsByStudentIdAsync(Guid studentId)
    {
        return await _db.Payments
            .Where(p => p.StudentId == studentId)
            .Include(p => p.Teacher)
            .Include(p => p.Lesson)
            .ToListAsync();
    }
}