using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;

namespace WebApplication1.Repositories.SubscriptionRepositories;

/// <summary>
/// Repository implementation for managing subscription data in the database.
/// </summary>
public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Initializes a new instance of the SubscriptionRepository class.
    /// </summary>
    /// <param name="db">The database context to use.</param>
    public SubscriptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Gets a subscription by its unique identifier.
    /// </summary>
    /// <param name="id">The subscription identifier.</param>
    /// <returns>The subscription with related teacher, student and subject if found, otherwise null.</returns>
    public async Task<Subscription> GetByIdAsync(Guid id)
    {
        return await _db.Subscriptions
            .Include(s => s.Teacher)
            .Include(s => s.Student)
            .Include(s => s.Subject)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// Gets all subscriptions in the system with related entities.
    /// </summary>
    /// <returns>A collection of all subscriptions with their teachers, students and subjects.</returns>
    public async Task<IEnumerable<Subscription>> GetAllAsync()
    {
        return await _db.Subscriptions
            .Include(s => s.Teacher)
            .Include(s => s.Student)
            .Include(s => s.Subject)
            .ToListAsync();
    }

    /// <summary>
    /// Gets all subscriptions for a specific student with related teacher and subject information.
    /// </summary>
    /// <param name="studentId">The student identifier.</param>
    /// <returns>A collection of the student's subscriptions.</returns>
    public async Task<IEnumerable<Subscription>> GetByStudentIdAsync(Guid studentId)
    {
        return await _db.Subscriptions
            .Include(s => s.Teacher)
            .Include(s => s.Subject)
            .Where(s => s.StudentId == studentId)
            .ToListAsync();
    }

    /// <summary>
    /// Gets all subscriptions for a specific teacher with related student and subject information.
    /// </summary>
    /// <param name="teacherId">The teacher identifier.</param>
    /// <returns>A collection of the teacher's subscriptions.</returns>
    public async Task<IEnumerable<Subscription>> GetByTeacherIdAsync(Guid teacherId)
    {
        return await _db.Subscriptions
            .Include(s => s.Student)
            .Include(s => s.Subject)
            .Where(s => s.TeacherId == teacherId)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new subscription to the database.
    /// </summary>
    /// <param name="subscription">The subscription to add.</param>
    public async Task AddAsync(Subscription subscription)
    {
        await _db.Subscriptions.AddAsync(subscription);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing subscription in the database.
    /// </summary>
    /// <param name="subscription">The subscription with updated information.</param>
    public async Task UpdateAsync(Subscription subscription)
    {
        _db.Subscriptions.Update(subscription);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a subscription from the database.
    /// </summary>
    /// <param name="id">The subscription identifier to delete.</param>
    public async Task DeleteAsync(Guid id)
    {
        var subscription = await GetByIdAsync(id);
        if (subscription != null)
        {
            _db.Subscriptions.Remove(subscription);
            await _db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Checks if a subscription exists in the database.
    /// </summary>
    /// <param name="id">The subscription identifier to check.</param>
    /// <returns>True if the subscription exists, otherwise false.</returns>
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _db.Subscriptions.AnyAsync(s => s.Id == id);
    }
}