using tutorCrm.Models;

namespace WebApplication1.Repositories.PaymentRepositories;

public interface IPaymentRepository
{
    Task<Payment?> GetPaymentByIdAsync(Guid id);
    Task<List<Payment>> GetAllPaymentsAsync();
    Task<Payment> CreatePaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
    Task<bool> PaymentExistsAsync(Guid id);
    Task DeletePaymentAsync(Guid id);
    Task<List<Payment>> GetPaymentsByTeacherIdAsync(Guid teacherId);
    Task<List<Payment>> GetPaymentsByStudentIdAsync(Guid studentId);
}