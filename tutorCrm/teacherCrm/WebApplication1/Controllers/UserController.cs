using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tutorCrm.Models;
using WebApplication1.Dtos.UserDtos;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

/// <summary>
/// Контроллер для управления пользователями системы.
/// Доступ к действиям ограничен ролями и авторизацией.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор контроллера пользователей.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Получить пользователя по его <paramref name="id"/>.
    /// Доступно самому пользователю или администратору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Пользователь или ошибка доступа.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationUser>> GetUser(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userId != id)
            return Forbid();

        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Получить список всех пользователей.
    /// Доступно только администраторам.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ApplicationUser>>> GetAllUsers()
    {
        if (!User.IsInRole("Admin"))
            return Forbid();

        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Создать нового пользователя.
    /// Доступно только администраторам.
    /// </summary>
    /// <param name="userDto">Данные нового пользователя.</param>
    /// <returns>Созданный пользователь.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        try
        {
            var user = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Обновить данные пользователя.
    /// Доступно самому пользователю или администратору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="user">Обновлённые данные пользователя.</param>
    /// <returns>Результат операции.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] ApplicationUser user)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userId != id)
            return Forbid();

        if (id != user.Id)
            return BadRequest("ID mismatch");

        try
        {
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Удалить пользователя.
    /// Доступно только администраторам.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
