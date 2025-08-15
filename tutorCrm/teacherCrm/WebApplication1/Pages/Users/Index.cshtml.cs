using Microsoft.AspNetCore.Mvc.RazorPages;
using tutorCrm.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class UsersIndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UsersIndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient();

        // ���� ����������� JWT �����������, ������ ����� �� ���� ��� HttpContext
        var token = HttpContext.Request.Cookies[".AspNetCore.Identity.Application"];
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            // ����� �������� ���������� ����� API
            var response = await client.GetAsync("https://localhost:5001/api/User");

            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
                if (users != null)
                    Users = users;
            }
            else
            {
                // ��������� ������
                Console.WriteLine($"������ �������: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"������ ��� ������ API: {ex.Message}");
        }
    }
}
