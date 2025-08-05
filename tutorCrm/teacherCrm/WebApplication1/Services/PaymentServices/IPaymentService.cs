using WebApplication1.Dtos.PaymentDtos;

namespace WebApplication1.Services.PaymentServices;

public interface IPaymentService
{
    Task<PaymentDto> GetPaymentByIdAsync(Guid id);
    Task<List<PaymentDto>> GetAllPaymentsAsync();
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto paymentDto);
    Task UpdatePaymentAsync(Guid id, UpdatePaymentDto paymentDto);
    Task DeletePaymentAsync(Guid id);
    Task<List<PaymentDto>> GetPaymentsByTeacherIdAsync(Guid teacherId);
    Task<List<PaymentDto>> GetPaymentsByStudentIdAsync(Guid studentId);
}