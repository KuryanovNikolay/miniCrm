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

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var roleName = string.IsNullOrWhiteSpace(createUserDto.Role) ? "User" : createUserDto.Role;

        // Создаем пользователя без UserManager (чтобы потом вручную управлять ролями)
        var user = new ApplicationUser
        {
            UserName = createUserDto.Username,
            Email = createUserDto.Email,
            FullName = createUserDto.FullName,
            PhoneNumber = createUserDto.PhoneNumber,
            ParentFullName = createUserDto.ParentFullName,
            ParentContact = createUserDto.ParentContact,
            RegistrationDate = DateTime.UtcNow,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        // Хешируем пароль вручную (UserManager обычно это делает)
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, createUserDto.Password);

        // Сохраняем пользователя в БД напрямую
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(); // user.Id становится доступен

        // Ищем роль в БД
        var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
        if (role == null)
        {
            // Создаем роль если нет
            role = new IdentityRole<Guid> { Name = roleName, NormalizedName = roleName.ToUpper() };
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
        }

        var userForRole = _dbContext.Users.FirstOrDefault(u => u.UserName == createUserDto.Username);
        

        // Добавляем связь в AspNetUserRoles
        var userRole = new IdentityUserRole<Guid>
        {
            UserId = userForRole.Id,
            RoleId = role.Id
        };
        _dbContext.UserRoles.Add(userRole);
        await _dbContext.SaveChangesAsync();

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

    // Новый метод для проверки ролей
    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    // Новый метод для получения всех ролей пользователя
    public async Task<IList<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user != null
            ? await _userManager.GetRolesAsync(user)
            : throw new KeyNotFoundException("Пользователь не найден");
    }
}