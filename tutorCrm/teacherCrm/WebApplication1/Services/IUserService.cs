using WebApplication1.Dtos.UserDtos;

namespace WebApplication1.Services;

/// <summary>
/// Интерфейс сервиса для работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Объект пользователя или null, если не найден.</returns>
    Task<ApplicationUser?> GetUserByIdAsync(Guid id);

    /// <summary>
    /// Получает список всех пользователей.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    Task<List<ApplicationUser>> GetAllUsersAsync();

    /// <summary>
    /// Регистрирует нового пользователя.
    /// </summary>
    /// <param name="dto">DTO с данными для регистрации.</param>
    /// <returns>DTO с информацией о зарегистрированном пользователе.</returns>
    Task<UserResponseDto> RegisterAsync(RegisterUserDto dto);

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="userDto">DTO с данными пользователя.</param>
    /// <returns>DTO с информацией о созданном пользователе.</returns>
    Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto);

    /// <summary>
    /// Обновляет данные пользователя.
    /// </summary>
    /// <param name="user">Объект пользователя с обновленными данными.</param>
    Task UpdateUserAsync(ApplicationUser user);

    /// <summary>
    /// Удаляет пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    Task DeleteUserAsync(Guid id);
}
