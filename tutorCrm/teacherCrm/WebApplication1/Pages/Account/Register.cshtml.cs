using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using WebApplication1.Dtos.LoginDtos;
using WebApplication1.Dtos.UserDtos;

/// <summary>
/// Модель страницы регистрации пользователя.
/// </summary>
public class RegisterModel : PageModel
{
    /// <summary>
    /// Модель данных для регистрации пользователя.
    /// </summary>
    [BindProperty]
    public RegisterUserDto Input { get; set; } = new();

    /// <summary>
    /// Сообщение об успешной регистрации.
    /// </summary>
    public string? SuccessMessage { get; set; }

    /// <summary>
    /// Сообщение об ошибке при регистрации.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Обрабатывает GET-запрос к странице регистрации.
    /// </summary>
    public void OnGet() { }

    /// <summary>
    /// Обрабатывает POST-запрос при отправке формы регистрации.
    /// </summary>
    /// <returns>
    /// Страницу регистрации с результатом операции.
    /// </returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001/");

            var response = await httpClient.PostAsJsonAsync("account/register", Input);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Регистрация прошла успешно!";
                Input = new();
            }
            else
            {
                ErrorMessage = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка: {ex.Message}";
        }

        return Page();
    }
}