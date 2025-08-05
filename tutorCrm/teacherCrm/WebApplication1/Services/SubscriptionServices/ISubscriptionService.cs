using WebApplication1.Dtos.SubscriptionsDtos;

namespace WebApplication1.Services.SubscriptionServices;

public interface ISubscriptionService
{
    Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id);
    Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
    Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByStudentIdAsync(Guid studentId);
    Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByTeacherIdAsync(Guid teacherId);
    Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto createDto);
    Task<SubscriptionDto> UpdateSubscriptionAsync(Guid id, UpdateSubscriptionDto updateDto);
    Task DeleteSubscriptionAsync(Guid id);
}
