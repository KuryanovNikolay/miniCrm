using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Dtos.PaymentDtos;
using WebApplication1.Services.PaymentServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

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
