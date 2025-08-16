using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Dtos.LessonDtos;
using WebApplication1.Services.LessonServices;

namespace WebApplication1.Controllers;

/// <summary>
/// Контроллер для управления занятиями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LessonsController : ControllerBase
{
    /// <summary>
    /// Сервис для работы с уроками.
    /// </summary>
    private readonly ILessonService _lessonService;

    /// <summary>
    /// Конструктор контроллера уроков.
    /// </summary>
    /// <param name="lessonService">Сервис для управления уроками.</param>
    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    /// <summary>
    /// Получает урок по его идентификатору.
    /// Доступ ограничен: просматривать могут администратор, преподаватель (связанный с уроком) или студент (назначенный на урок).
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>Объект урока или код ошибки.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLessonById(Guid id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (!User.IsInRole("Admin") &&
            lesson.TeacherId != userId &&
            lesson.StudentId != userId)
        {
            return Forbid();
        }

        return Ok(lesson);
    }

    /// <summary>
    /// Получает список уроков.
    /// Доступ зависит от роли:
    /// - Администратор видит все уроки,
    /// - Преподаватель видит только свои уроки,
    /// - Студент видит только свои уроки.
    /// </summary>
    /// <returns>Список уроков.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllLessons()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Admin"))
        {
            return Ok(await _lessonService.GetAllLessonsAsync());
        }
        else if (User.IsInRole("Teacher"))
        {
            return Ok(await _lessonService.GetLessonsByTeacherIdAsync(userId));
        }
        else if (User.IsInRole("Student"))
        {
            return Ok(await _lessonService.GetLessonsByStudentIdAsync(userId));
        }

        return Forbid();
    }

    /// <summary>
    /// Создает новый урок.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может создавать уроки только для себя.
    /// </summary>
    /// <param name="dto">Данные для создания урока.</param>
    /// <returns>Созданный урок.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && dto.TeacherId != userId)
            return Forbid();

        var createdLesson = await _lessonService.CreateLessonAsync(dto);
        return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Id }, createdLesson);
    }

    /// <summary>
    /// Обновляет существующий урок.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может изменять только свои уроки.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <param name="dto">Обновленные данные урока.</param>
    /// <returns>Код 204 (No Content) при успешном обновлении.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] UpdateLessonDto dto)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && lesson.TeacherId != userId)
            return Forbid();

        await _lessonService.UpdateLessonAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет урок.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может удалять только свои уроки.
    /// </summary>
    /// <param name="id">Идентификатор урока.</param>
    /// <returns>Код 204 (No Content) при успешном удалении.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Teacher") && lesson.TeacherId != userId)
            return Forbid();

        await _lessonService.DeleteLessonAsync(id);
        return NoContent();
    }
}
