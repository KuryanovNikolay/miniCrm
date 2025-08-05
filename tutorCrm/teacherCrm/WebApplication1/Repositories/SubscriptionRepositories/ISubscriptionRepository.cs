using tutorCrm.Models;

namespace WebApplication1.Repositories.SubscriptionRepositories;

public interface ISubscriptionRepository
{
    Task<Subscription> GetByIdAsync(Guid id);
    Task<IEnumerable<Subscription>> GetAllAsync();
    Task<IEnumerable<Subscription>> GetByStudentIdAsync(Guid studentId);
    Task<IEnumerable<Subscription>> GetByTeacherIdAsync(Guid teacherId);
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
