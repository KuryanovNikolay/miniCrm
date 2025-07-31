using WebApplication1.Dtos.RolesDtos;

namespace WebApplication1.Services.RoleServices;

public interface IRoleService
{
    Task<RoleDto> GetRoleByIdAsync(Guid id);
    Task<List<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> CreateRoleAsync(CreateRoleDto roleDto);
    Task UpdateRoleAsync(Guid id, UpdateRoleDto roleDto);
    Task DeleteRoleAsync(Guid id);

    Task AssignRoleToUserAsync(AssignRoleDto assignRoleDto);
    Task RemoveRoleFromUserAsync(RemoveRoleDto removeRoleDto);
    Task<List<RoleDto>> GetUserRolesAsync(Guid userId);
    Task<bool> UserHasRoleAsync(Guid userId, Guid roleId);
    Task<int> AssignRolesToUserAsync(Guid userId, IEnumerable<Guid> roleIds);
}
