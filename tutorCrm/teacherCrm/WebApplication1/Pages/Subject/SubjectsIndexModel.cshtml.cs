using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebApplication1.Dtos.SubjectDtos;

public class SubjectsIndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SubjectsIndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<SubjectDto> Subjects { get; set; } = new();

    [TempData]
    public string? Message { get; set; }

    // ��������� ���� ���������
    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient();

        try
        {
            var response = await client.GetAsync("https://localhost:7214/api/Subjects");
            if (response.IsSuccessStatusCode)
            {
                var subjects = await response.Content.ReadFromJsonAsync<List<SubjectDto>>();
                if (subjects != null)
                    Subjects = subjects;
            }
            else
            {
                Message = "�� ������� ��������� ������ ���������.";
            }
        }
        catch
        {
            Message = "������ ��� ����������� � �������.";
        }
    }


    // �������� �� �������
    public async Task<IActionResult> OnPostSubscribeAsync(Guid id)
    {
        var client = _httpClientFactory.CreateClient();
        var token = HttpContext.Request.Cookies[".AspNetCore.Identity.Application"];
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            var response = await client.PostAsync($"https://localhost:5001/api/Subjects/{id}/subscribe", null);
            if (response.IsSuccessStatusCode)
                Message = "�� ������� ����������� �� �������!";
            else
                Message = "�� ������� ����������� �� �������.";
        }
        catch
        {
            Message = "������ ��� ����������� � �������.";
        }

        return RedirectToPage();
    }
}
