using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.PaymentDtos;
using WebApplication1.Services.PaymentServices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return Ok(payment);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        return Ok(payments);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
    {
        var createdPayment = await _paymentService.CreatePaymentAsync(dto);
        return CreatedAtAction(nameof(GetPaymentById), new { id = createdPayment.Id }, createdPayment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] UpdatePaymentDto dto)
    {
        await _paymentService.UpdatePaymentAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        await _paymentService.DeletePaymentAsync(id);
        return NoContent();
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetPaymentsByTeacherId(Guid teacherId)
    {
        var payments = await _paymentService.GetPaymentsByTeacherIdAsync(teacherId);
        return Ok(payments);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetPaymentsByStudentId(Guid studentId)
    {
        var payments = await _paymentService.GetPaymentsByStudentIdAsync(studentId);
        return Ok(payments);
    }
}