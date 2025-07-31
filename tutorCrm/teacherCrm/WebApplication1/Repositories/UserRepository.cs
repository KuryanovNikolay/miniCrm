using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;

namespace WebApplication1.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _db.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _db.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<bool> UserExistsAsync(Guid id)
    {
        return await _db.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> UserExistsAsync(string username, string email)
    {
        return await _db.Users
            .AnyAsync(u => u.Username == username || u.Email == email);
    }

    public async Task AddUserAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
    {
        var user = await GetUserByIdAsync(userId);
        return user?.UserRoles.Any(ur => ur.Role.Name == roleName) ?? false;
    }
}