using Microsoft.AspNetCore.Mvc.RazorPages;
using tutorCrm.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

/// <summary>
/// ������ �������� ��� ����������� ������ �������������.
/// </summary>
public class UsersIndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// �������������� ����� ��������� ������ <see cref="UsersIndexModel"/>.
    /// </summary>
    /// <param name="httpClientFactory">������� ��� �������� HTTP-��������.</param>
    public UsersIndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// ������ ������������� ��� ����������� �� ��������.
    /// </summary>
    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    /// <summary>
    /// ������������ GET-������ ��� ��������� ������ �������������.
    /// </summary>
    /// <remarks>
    /// ����� ��������� ��������� ��������:
    /// 1. ������� HTTP-������ � ������� �������
    /// 2. ��������� ����� �������������� �� cookies, ���� �� ����
    /// 3. ���������� ������ � API ��� ��������� ������ �������������
    /// 4. ������������ ����� � ��������� ���������� �������������
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
                Console.WriteLine($"������ �������: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"������ ��� ������ API: {ex.Message}");
        }
    }
}