using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;

namespace WebApplication1.Repositories.RoleRepositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _db;

    public RoleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Role?> GetRoleByIdAsync(Guid id)
    {
        return await _db.Roles
            .Include(r => r.UserRoles)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        return await _db.Roles
            .Include(r => r.UserRoles)
            .ToListAsync();
    }

    public async Task<Role> CreateRoleAsync(Role role)
    {
        await _db.Roles.AddAsync(role);
        await _db.SaveChangesAsync();
        return role;
    }

    public async Task UpdateRoleAsync(Role role)
    {
        _db.Roles.Update(role);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> RoleExistsAsync(Guid id)
    {
        return await _db.Roles.AnyAsync(r => r.Id == id);
    }

    public async Task DeleteRoleAsync(Guid id)
    {
        var role = await GetRoleByIdAsync(id);
        if (role != null)
        {
            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId)
    {
        var user = await _db.Users.FindAsync(userId);
        var role = await _db.Roles.FindAsync(roleId);

        if (user == null || role == null)
            return false;

        var existingRelation = await _db.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (existingRelation != null)
            return false;

        _db.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<int> AssignRolesToUserAsync(Guid userId, IEnumerable<Guid> roleIds)
    {
        // 1. Проверяем существование пользователя
        var userExists = await _db.Users.AnyAsync(u => u.Id == userId);
        if (!userExists) return 0;

        // 2. Получаем существующие роли пользователя
        var existingRoleIds = await _db.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

        // 3. Фильтруем только новые роли
        var newRoleIds = roleIds
            .Distinct()
            .Where(roleId => !existingRoleIds.Contains(roleId))
            .ToList();

        if (!newRoleIds.Any())
            return 0;

        // 4. Проверяем существование указанных ролей
        var existingRolesCount = await _db.Roles
            .Where(r => newRoleIds.Contains(r.Id))
            .CountAsync();

        if (existingRolesCount != newRoleIds.Count)
            return 0;

        // 5. Создаем новые связи
        var newUserRoles = newRoleIds.Select(roleId => new UserRole
        {
            UserId = userId,
            RoleId = roleId
        });

        await _db.UserRoles.AddRangeAsync(newUserRoles);
        return await _db.SaveChangesAsync();
    }

    public async Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId)
    {
        var userRole = await _db.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (userRole == null)
            return false;

        _db.UserRoles.Remove(userRole);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<Role>> GetUserRolesAsync(Guid userId)
    {
        return await _db.UserRoles
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<bool> UserHasRoleAsync(Guid userId, Guid roleId)
    {
        return await _db.UserRoles
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }
}
