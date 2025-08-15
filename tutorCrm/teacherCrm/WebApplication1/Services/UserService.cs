using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly ILogger<UserService> _logger;
    private readonly ApplicationDbContext _dbContext;

    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        ILogger<UserService> logger,
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await Task.Run(() => _userManager.Users.ToList());
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
    {
        var roleName = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role;

        var user = new ApplicationUser
        {
            UserName = dto.Username,
            Email = dto.Email,
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            ParentFullName = dto.ParentFullName,
            ParentContact = dto.ParentContact,
            RegistrationDate = DateTime.UtcNow
        };

        var createResult = await _userManager.CreateAsync(user, dto.Password);
        if (!createResult.Succeeded)
            throw new Exception(string.Join(", ", createResult.Errors.Select(e => e.Description)));

        if (!await _roleManager.RoleExistsAsync(roleName))
            await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));

        await _userManager.AddToRoleAsync(user, roleName);

        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            RegistrationDate = user.RegistrationDate,
            ParentFullName = user.ParentFullName,
            ParentContact = user.ParentContact,
            Role = roleName
        };
    }





    public async Task UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Ошибка обновления пользователя: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new KeyNotFoundException("Пользователь не найден");
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Ошибка удаления пользователя: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<IList<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user != null
            ? await _userManager.GetRolesAsync(user)
            : throw new KeyNotFoundException("Пользователь не найден");
    }

    public async Task<UserResponseDto> RegisterAsync(RegisterUserDto dto)
    {
        var createDto = new CreateUserDto
        {
            Username = dto.Username,
            Email = dto.Email,
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            ParentFullName = dto.ParentFullName,
            ParentContact = dto.ParentContact,
            Password = dto.Password,
            Role = "User" 
        };

        return await CreateUserAsync(createDto);
    }

}