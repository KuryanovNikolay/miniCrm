using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Dtos.PaymentDtos;
using WebApplication1.Services.PaymentServices;

namespace WebApplication1.Controllers;

/// <summary>
/// Контроллер для управления платежами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    /// <summary>
    /// Сервис для работы с платежами.
    /// </summary>
    private readonly IPaymentService _paymentService;

    /// <summary>
    /// Конструктор контроллера платежей.
    /// </summary>
    /// <param name="paymentService">Сервис для управления платежами.</param>
    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    /// <summary>
    /// Получает платеж по идентификатору.
    /// Доступ имеют администратор, преподаватель (участвующий в платеже) или студент (участвующий в платеже).
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>Объект платежа или код ошибки.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(Guid id)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(id);
        if (payment == null)
            return NotFound();

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (!User.IsInRole("Admin") &&
            payment.TeacherId != userId &&
            payment.StudentId != userId)
        {
            return Forbid();
        }

        return Ok(payment);
    }

    /// <summary>
    /// Получает список всех платежей.
    /// Доступ зависит от роли:
    /// - Администратор видит все платежи,
    /// - Преподаватель видит только свои платежи,
    /// - Студент видит только свои платежи.
    /// </summary>
    /// <returns>Список платежей.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (User.IsInRole("Admin"))
        {
            return Ok(await _paymentService.GetAllPaymentsAsync());
        }
        else if (User.IsInRole("Teacher"))
        {
            return Ok(await _paymentService.GetPaymentsByTeacherIdAsync(userId));
        }
        else if (User.IsInRole("Student"))
        {
            return Ok(await _paymentService.GetPaymentsByStudentIdAsync(userId));
        }

        return Forbid();
    }

    /// <summary>
    /// Создает новый платеж.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может создавать только свои платежи.
    /// </summary>
    /// <param name="dto">Данные для создания платежа.</param>
    /// <returns>Созданный платеж.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
    {
        if (User.IsInRole("Teacher"))
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (dto.TeacherId != userId)
                return Forbid();
        }

        var createdPayment = await _paymentService.CreatePaymentAsync(dto);
        return CreatedAtAction(nameof(GetPaymentById), new { id = createdPayment.Id }, createdPayment);
    }

    /// <summary>
    /// Обновляет существующий платеж.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может изменять только свои платежи.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <param name="dto">Обновленные данные платежа.</param>
    /// <returns>Код 204 (No Content) при успешном обновлении.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] UpdatePaymentDto dto)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(id);
        if (payment == null)
            return NotFound();

        if (User.IsInRole("Teacher"))
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (payment.TeacherId != userId)
                return Forbid();
        }

        await _paymentService.UpdatePaymentAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет платеж.
    /// Доступно для администратора и преподавателя.
    /// Преподаватель может удалять только свои платежи.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>Код 204 (No Content) при успешном удалении.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(id);
        if (payment == null)
            return NotFound();

        if (User.IsInRole("Teacher"))
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (payment.TeacherId != userId)
                return Forbid();
        }

        await _paymentService.DeletePaymentAsync(id);
        return NoContent();
    }
}
