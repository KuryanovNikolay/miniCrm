using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.SubscriptionsDtos;
using WebApplication1.Repositories.SubscriptionRepositories;

namespace WebApplication1.Services.SubscriptionServices
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _subscriptionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByStudentIdAsync(Guid studentId)
        {
            var subscriptions = await _subscriptionRepository.GetByStudentIdAsync(studentId);
            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByTeacherIdAsync(Guid teacherId)
        {
            var subscriptions = await _subscriptionRepository.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto createDto)
        {
            var subscription = _mapper.Map<Subscription>(createDto);

            // Set default values
            subscription.LessonsLeft = subscription.LessonsTotal;
            subscription.StartDate = createDto.StartDate ?? DateTime.UtcNow.Date;

            await _subscriptionRepository.AddAsync(subscription);
            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task<SubscriptionDto> UpdateSubscriptionAsync(Guid id, UpdateSubscriptionDto updateDto)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            if (subscription == null)
            {
                throw new KeyNotFoundException($"Subscription with id {id} not found");
            }

            if (updateDto.LessonsTotal.HasValue)
            {
                subscription.LessonsTotal = updateDto.LessonsTotal.Value;
            }

            if (updateDto.LessonsLeft.HasValue)
            {
                subscription.LessonsLeft = updateDto.LessonsLeft.Value;
            }

            if (updateDto.Price.HasValue)
            {
                subscription.Price = updateDto.Price.Value;
            }

            if (updateDto.EndDate.HasValue)
            {
                subscription.EndDate = updateDto.EndDate.Value;
            }

            await _subscriptionRepository.UpdateAsync(subscription);
            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task DeleteSubscriptionAsync(Guid id)
        {
            await _subscriptionRepository.DeleteAsync(id);
        }
    }
}
