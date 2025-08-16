using tutorCrm.Models;

namespace WebApplication1.Repositories.SubscriptionRepositories;

/// <summary>
/// Interface for managing subscription data operations.
/// </summary>
public interface ISubscriptionRepository
{
    /// <summary>
    /// Gets a subscription by its unique identifier.
    /// </summary>
    /// <param name="id">The subscription identifier.</param>
    /// <returns>The subscription if found, otherwise null.</returns>
    Task<Subscription> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets all subscriptions in the system.
    /// </summary>
    /// <returns>A collection of all subscriptions.</returns>
    Task<IEnumerable<Subscription>> GetAllAsync();

    /// <summary>
    /// Gets all subscriptions for a specific student.
    /// </summary>
    /// <param name="studentId">The student identifier.</param>
    /// <returns>A collection of the student's subscriptions.</returns>
    Task<IEnumerable<Subscription>> GetByStudentIdAsync(Guid studentId);

    /// <summary>
    /// Gets all subscriptions for a specific teacher.
    /// </summary>
    /// <param name="teacherId">The teacher identifier.</param>
    /// <returns>A collection of the teacher's subscriptions.</returns>
    Task<IEnumerable<Subscription>> GetByTeacherIdAsync(Guid teacherId);

    /// <summary>
    /// Adds a new subscription to the system.
    /// </summary>
    /// <param name="subscription">The subscription to add.</param>
    Task AddAsync(Subscription subscription);

    /// <summary>
    /// Updates an existing subscription.
    /// </summary>
    /// <param name="subscription">The subscription with updated information.</param>
    Task UpdateAsync(Subscription subscription);

    /// <summary>
    /// Deletes a subscription by its identifier.
    /// </summary>
    /// <param name="id">The subscription identifier to delete.</param>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Checks if a subscription exists in the system.
    /// </summary>
    /// <param name="id">The subscription identifier to check.</param>
    /// <returns>True if the subscription exists, otherwise false.</returns>
    Task<bool> ExistsAsync(Guid id);
}
