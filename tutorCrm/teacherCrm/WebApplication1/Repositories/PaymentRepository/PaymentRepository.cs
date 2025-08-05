using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;
using WebApplication1.Repositories.PaymentRepositories;

namespace WebApplication1.Repositories.PaymentRepositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Payment?> GetPaymentByIdAsync(Guid id)
    {
        return await _db.Payments
            .Include(p => p.Teacher)
            .Include(p => p.Student)
            .Include(p => p.Lesson)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await _db.Payments
            .Include(p => p.Teacher)
            .Include(p => p.Student)
            .Include(p => p.Lesson)
            .ToListAsync();
    }

    public async Task<Payment> CreatePaymentAsync(Payment payment)
    {
        payment.Status = "Pending"; // Default status
        payment.PaymentDate = DateTime.UtcNow;
        await _db.Payments.AddAsync(payment);
        await _db.SaveChangesAsync();
        return payment;
    }

    public async Task UpdatePaymentAsync(Payment payment)
    {
        _db.Payments.Update(payment);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> PaymentExistsAsync(Guid id)
    {
        return await _db.Payments.AnyAsync(p => p.Id == id);
    }

    public async Task DeletePaymentAsync(Guid id)
    {
        var payment = await GetPaymentByIdAsync(id);
        if (payment != null)
        {
            _db.Payments.Remove(payment);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<Payment>> GetPaymentsByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Payments
            .Where(p => p.TeacherId == teacherId)
            .Include(p => p.Student)
            .Include(p => p.Lesson)
            .ToListAsync();
    }

    public async Task<List<Payment>> GetPaymentsByStudentIdAsync(Guid studentId)
    {
        return await _db.Payments
            .Where(p => p.StudentId == studentId)
            .Include(p => p.Teacher)
            .Include(p => p.Lesson)
            .ToListAsync();
    }
}