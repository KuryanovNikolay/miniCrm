using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;

namespace WebApplication1.Repositories.SubscriptionRepositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _db;

    public SubscriptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Subscription> GetByIdAsync(Guid id)
    {
        return await _db.Subscriptions
            .Include(s => s.Teacher)
            .Include(s => s.Student)
            .Include(s => s.Subject)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        return await _db.Subscriptions
            .Include(s => s.Teacher)
            .Include(s => s.Student)
            .Include(s => s.Subject)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetByStudentIdAsync(Guid studentId)
    {
        return await _db.Subscriptions
            .Include(s => s.Teacher)
            .Include(s => s.Subject)
            .Where(s => s.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Subscriptions
            .Include(s => s.Student)
            .Include(s => s.Subject)
            .Where(s => s.TeacherId == teacherId)
            .ToListAsync();
    }

    public async Task AddAsync(Subscription subscription)
    {
        await _db.Subscriptions.AddAsync(subscription);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        _db.Subscriptions.Update(subscription);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var subscription = await GetByIdAsync(id);
        if (subscription != null)
        {
            _db.Subscriptions.Remove(subscription);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _db.Subscriptions.AnyAsync(s => s.Id == id);
    }
}
