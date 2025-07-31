using tutorCrm.Models;

namespace WebApplication1.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<List<User>> GetAllUsersAsync();
    Task<bool> UserExistsAsync(Guid id);
    Task<bool> UserExistsAsync(string username, string email);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
    Task<bool> IsUserInRoleAsync(Guid userId, string roleName);

}