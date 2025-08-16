using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.LoginDtos;
using WebApplication1.Dtos.UserDtos;

[Route("account")]
[ApiController]
/// <summary>
/// Контроллер для управления учетными записями пользователей.
/// </summary>
public class AccountController : ControllerBase
{
    /// <summary>
    /// Менеджер для управления процессом входа пользователей.
    /// </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;
    /// <summary>
    /// Менеджер для управления пользователями.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;
    /// <summary>
    /// Менеджер для работы с ролями.
    /// </summary>
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    /// <summary>
    /// Конструктор контроллера учетных записей.
    /// </summary>
    /// <param name="signInManager">Менеджер входа пользователей.</param>
    /// <param name="userManager">Менеджер пользователей.</param>
    /// <param name="roleManager">Менеджер ролей.</param>
    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Регистрирует нового пользователя.
    /// </summary>
    /// <param name="model">Данные для регистрации.</param>
    /// <returns>Результат операции регистрации.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) 
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, "User");

        return Ok(new { message = "Регистрация успешна" });
    }

    /// <summary>
    /// Выполняет вход пользователя.
    /// </summary>
    /// <param name="model">Данные для входа (email, пароль).</param>
    /// <returns>Результат входа.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return Ok(new { message = "Login successful" });
            }
        }

        return Unauthorized(new { message = "Invalid email or password" });
    }

    /// <summary>
    /// Выполняет выход пользователя из системы.
    /// </summary>
    /// <returns>Код 204 (No Content) при успешном выходе.</returns>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }
}
