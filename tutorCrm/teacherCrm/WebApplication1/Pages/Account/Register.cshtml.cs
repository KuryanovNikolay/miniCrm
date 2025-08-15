using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using WebApplication1.Dtos.LoginDtos;

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterUserDto Input { get; set; } = new();

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }

    public void OnGet() { }

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
