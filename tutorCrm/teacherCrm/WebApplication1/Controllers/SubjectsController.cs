using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Dtos.SubjectDtos;
using WebApplication1.Services.SubjectServices;

namespace WebApplication1.Controllers;

/// <summary>
/// Контроллер для управления учебными предметами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    /// <summary>
    /// Сервис для работы с предметами.
    /// </summary>
    private readonly ISubjectService _subjectService;

    /// <summary>
    /// Конструктор контроллера предметов.
    /// </summary>
    /// <param name="subjectService">Сервис для управления предметами.</param>
    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    /// <summary>
    /// Получает предмет по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>Объект предмета или код ошибки.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(Guid id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null)
            return NotFound();

        return Ok(subject);
    }

    /// <summary>
    /// Получает список всех предметов.
    /// Доступ разрешён анонимным пользователям.
    /// </summary>
    /// <returns>Список предметов.</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllSubjects()
    {
        var subjects = await _subjectService.GetAllSubjectsAsync();
        return Ok(subjects);
    }

    /// <summary>
    /// Создает новый предмет.
    /// Доступно только для преподавателя.
    /// Преподаватель может создавать предметы только для себя.
    /// </summary>
    /// <param name="dto">Данные для создания предмета.</param>
    /// <returns>Созданный предмет.</returns>
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (dto.TeacherId != userId)
            return Forbid();

        var createdSubject = await _subjectService.CreateSubjectAsync(dto);
        return CreatedAtAction(nameof(GetSubjectById), new { id = createdSubject.Id }, createdSubject);
    }

    /// <summary>
    /// Обновляет существующий предмет.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может изменять только свои предметы.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <param name="dto">Обновлённые данные предмета.</param>
    /// <returns>Код 204 (No Content) при успешном обновлении.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] UpdateSubjectDto dto)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && subject.TeacherId != userId)
            return Forbid();

        await _subjectService.UpdateSubjectAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет предмет.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может удалять только свои предметы.
    /// </summary>
    /// <param name="id">Идентификатор предмета.</param>
    /// <returns>Код 204 (No Content) при успешном удалении.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && subject.TeacherId != userId)
            return Forbid();

        await _subjectService.DeleteSubjectAsync(id);
        return NoContent();
    }
}
