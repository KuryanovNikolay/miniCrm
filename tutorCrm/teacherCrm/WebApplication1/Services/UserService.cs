using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services;

/// <summary>
/// Сервис для работы с пользователями системы.
/// </summary>
public class UserService : IUserService
{
    /// <summary>
    /// Менеджер пользователей.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// Менеджер ролей.
    /// </summary>
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<UserService> _logger;

    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса пользователей.
    /// </summary>
    /// <param name="userManager">Менеджер пользователей.</param>
    /// <param name="roleManager">Менеджер ролей.</param>
    /// <param name="logger">Логгер.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
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

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Объект пользователя или null, если не найден.</returns>
    public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    /// <summary>
    /// Получает список всех пользователей.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await Task.Run(() => _userManager.Users.ToList());
    }

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="dto">DTO с данными пользователя.</param>
    /// <returns>DTO с информацией о созданном пользователе.</returns>
    /// <exception cref="Exception">Если не удалось создать пользователя.</exception>
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

    /// <summary>
    /// Обновляет данные пользователя.
    /// </summary>
    /// <param name="user">Объект пользователя с обновленными данными.</param>
    /// <exception cref="InvalidOperationException">Если не удалось обновить пользователя.</exception>
    public async Task UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Ошибка обновления пользователя: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    /// <summary>
    /// Удаляет пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <exception cref="KeyNotFoundException">Если пользователь не найден.</exception>
    /// <exception cref="InvalidOperationException">Если не удалось удалить пользователя.</exception>
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

    /// <summary>
    /// Регистрирует нового пользователя.
    /// </summary>
    /// <param name="dto">DTO с данными для регистрации.</param>
    /// <returns>DTO с информацией о зарегистрированном пользователе.</returns>
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

    /// <summary>
    /// Проверяет существование роли.
    /// </summary>
    /// <param name="roleName">Название роли.</param>
    /// <returns>True, если роль существует, иначе False.</returns>
    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    /// <summary>
    /// Получает роли пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Список ролей пользователя.</returns>
    /// <exception cref="KeyNotFoundException">Если пользователь не найден.</exception>
    public async Task<IList<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user != null
            ? await _userManager.GetRolesAsync(user)
            : throw new KeyNotFoundException("Пользователь не найден");
    }
}