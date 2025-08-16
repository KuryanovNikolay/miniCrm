using Microsoft.AspNetCore.Mvc.RazorPages;
using tutorCrm.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

/// <summary>
/// Модель страницы для отображения списка пользователей.
/// </summary>
public class UsersIndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UsersIndexModel"/>.
    /// </summary>
    /// <param name="httpClientFactory">Фабрика для создания HTTP-клиентов.</param>
    public UsersIndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Список пользователей для отображения на странице.
    /// </summary>
    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    /// <summary>
    /// Обрабатывает GET-запрос для получения списка пользователей.
    /// </summary>
    /// <remarks>
    /// Метод выполняет следующие действия:
    /// 1. Создает HTTP-клиент с помощью фабрики
    /// 2. Добавляет токен аутентификации из cookies, если он есть
    /// 3. Отправляет запрос к API для получения списка пользователей
    /// 4. Обрабатывает ответ и сохраняет полученных пользователей
    /// </remarks>
    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient();

        var token = HttpContext.Request.Cookies[".AspNetCore.Identity.Application"];
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            var response = await client.GetAsync("https://localhost:5001/api/User");

            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
                if (users != null)
                    Users = users;
            }
            else
            {
                Console.WriteLine($"Ошибка запроса: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при вызове API: {ex.Message}");
        }
    }
}