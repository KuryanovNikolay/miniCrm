using tutorCrm.Models;

namespace WebApplication1.Repositories.RoleRepositories;

public interface IRoleRepository
{
    Task<Role?> GetRoleByIdAsync(Guid id);
    Task<List<Role>> GetAllRolesAsync();
    Task<Role> CreateRoleAsync(Role role);
    Task UpdateRoleAsync(Role role);
    Task DeleteRoleAsync(Guid id);
    Task<bool> RoleExistsAsync(Guid id);
    Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId);
    Task<int> AssignRolesToUserAsync(Guid userId, IEnumerable<Guid> roleIds);
    Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId);
    Task<List<Role>> GetUserRolesAsync(Guid userId);
    Task<bool> UserHasRoleAsync(Guid userId, Guid roleId);
}
