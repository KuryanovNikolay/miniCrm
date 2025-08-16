using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.SubscriptionsDtos;
using WebApplication1.Repositories.SubscriptionRepositories;

namespace WebApplication1.Services.SubscriptionServices;

/// <summary>
/// Сервис для работы с подписками.
/// </summary>
public class SubscriptionService : ISubscriptionService
{
    /// <summary>
    /// Репозиторий для работы с подписками.
    /// </summary>
    private readonly ISubscriptionRepository _subscriptionRepository;

    /// <summary>
    /// Объект маппинга DTO.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса подписок.
    /// </summary>
    /// <param name="subscriptionRepository">Репозиторий подписок.</param>
    /// <param name="mapper">Объект маппинга.</param>
    public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
    {
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает подписку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подписки.</param>
    /// <returns>DTO подписки.</returns>
    public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(id);
        return _mapper.Map<SubscriptionDto>(subscription);
    }

    /// <summary>
    /// Получает все подписки в системе.
    /// </summary>
    /// <returns>Коллекция DTO подписок.</returns>
    public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
    }

    /// <summary>
    /// Получает подписки по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Коллекция DTO подписок студента.</returns>
    public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByStudentIdAsync(Guid studentId)
    {
        var subscriptions = await _subscriptionRepository.GetByStudentIdAsync(studentId);
        return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
    }

    /// <summary>
    /// Получает подписки по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Коллекция DTO подписок преподавателя.</returns>
    public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByTeacherIdAsync(Guid teacherId)
    {
        var subscriptions = await _subscriptionRepository.GetByTeacherIdAsync(teacherId);
        return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
    }

    /// <summary>
    /// Создает новую подписку.
    /// </summary>
    /// <param name="createDto">DTO для создания подписки.</param>
    /// <returns>DTO созданной подписки.</returns>
    public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto createDto)
    {
        var subscription = _mapper.Map<Subscription>(createDto);

        // Установка значений по умолчанию
        subscription.LessonsLeft = subscription.LessonsTotal;
        subscription.StartDate = createDto.StartDate ?? DateTime.UtcNow.Date;

        await _subscriptionRepository.AddAsync(subscription);
        return _mapper.Map<SubscriptionDto>(subscription);
    }

    /// <summary>
    /// Обновляет существующую подписку.
    /// </summary>
    /// <param name="id">Идентификатор подписки.</param>
    /// <param name="updateDto">DTO с обновленными данными.</param>
    /// <returns>DTO обновленной подписки.</returns>
    /// <exception cref="KeyNotFoundException">Если подписка не найдена.</exception>
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

    /// <summary>
    /// Удаляет подписку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подписки.</param>
    public async Task DeleteSubscriptionAsync(Guid id)
    {
        await _subscriptionRepository.DeleteAsync(id);
    }
}
