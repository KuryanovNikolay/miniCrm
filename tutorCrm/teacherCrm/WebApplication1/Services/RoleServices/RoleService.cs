using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.RolesDtos;
using WebApplication1.Repositories;
using WebApplication1.Repositories.RoleRepositories;

namespace WebApplication1.Services.RoleServices;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RoleService(
        IRoleRepository roleRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<RoleDto> GetRoleByIdAsync(Guid id)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);
        if (role == null) throw new KeyNotFoundException("Role not found");
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllRolesAsync();
        return _mapper.Map<List<RoleDto>>(roles);
    }

    public async Task<RoleDto> CreateRoleAsync(CreateRoleDto roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);
        var createdRole = await _roleRepository.CreateRoleAsync(role);
        return _mapper.Map<RoleDto>(createdRole);
    }

    public async Task UpdateRoleAsync(Guid id, UpdateRoleDto roleDto)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);
        if (role == null) throw new KeyNotFoundException("Role not found");

        _mapper.Map(roleDto, role);
        await _roleRepository.UpdateRoleAsync(role);
    }

    public async Task DeleteRoleAsync(Guid id)
    {
        if (!await _roleRepository.RoleExistsAsync(id))
            throw new KeyNotFoundException("Role not found");

        await _roleRepository.DeleteRoleAsync(id);
    }

    public async Task AssignRoleToUserAsync(AssignRoleDto assignRoleDto)
    {
        if (!await _userRepository.UserExistsAsync(assignRoleDto.UserId))
            throw new KeyNotFoundException("User not found");

        if (!await _roleRepository.RoleExistsAsync(assignRoleDto.RoleId))
            throw new KeyNotFoundException("Role not found");

        if (!await _roleRepository.AssignRoleToUserAsync(assignRoleDto.UserId, assignRoleDto.RoleId))
            throw new InvalidOperationException("Failed to assign role");
    }

    public async Task RemoveRoleFromUserAsync(RemoveRoleDto removeRoleDto)
    {
        if (!await _roleRepository.RemoveRoleFromUserAsync(removeRoleDto.UserId, removeRoleDto.RoleId))
            throw new InvalidOperationException("Role assignment not found");
    }

    public async Task<List<RoleDto>> GetUserRolesAsync(Guid userId)
    {
        if (!await _userRepository.UserExistsAsync(userId))
            throw new KeyNotFoundException("User not found");

        var roles = await _roleRepository.GetUserRolesAsync(userId);
        return _mapper.Map<List<RoleDto>>(roles);
    }

    public async Task<bool> UserHasRoleAsync(Guid userId, Guid roleId)
    {
        return await _roleRepository.UserHasRoleAsync(userId, roleId);
    }

    public async Task<int> AssignRolesToUserAsync(Guid userId, IEnumerable<Guid> roleIds)
    {
        if (!await _userRepository.UserExistsAsync(userId))
            throw new KeyNotFoundException("User not found");

        return await _roleRepository.AssignRolesToUserAsync(userId, roleIds);
    }
}
