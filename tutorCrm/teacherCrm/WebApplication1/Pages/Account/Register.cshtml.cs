using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using WebApplication1.Dtos.LoginDtos;
using WebApplication1.Dtos.UserDtos;

/// <summary>
/// ������ �������� ����������� ������������.
/// </summary>
public class RegisterModel : PageModel
{
    /// <summary>
    /// ������ ������ ��� ����������� ������������.
    /// </summary>
    [BindProperty]
    public RegisterUserDto Input { get; set; } = new();

    /// <summary>
    /// ��������� �� �������� �����������.
    /// </summary>
    public string? SuccessMessage { get; set; }

    /// <summary>
    /// ��������� �� ������ ��� �����������.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// ������������ GET-������ � �������� �����������.
    /// </summary>
    public void OnGet() { }

    /// <summary>
    /// ������������ POST-������ ��� �������� ����� �����������.
    /// </summary>
    /// <returns>
    /// �������� ����������� � ����������� ��������.
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
                SuccessMessage = "����������� ������ �������!";
                Input = new();
            }
            else
            {
                ErrorMessage = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"������: {ex.Message}";
        }

        return Page();
    }
}