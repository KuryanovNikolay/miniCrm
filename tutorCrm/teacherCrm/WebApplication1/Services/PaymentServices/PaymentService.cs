using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.PaymentDtos;
using WebApplication1.Repositories.PaymentRepositories;

namespace WebApplication1.Services.PaymentServices;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<PaymentDto> GetPaymentByIdAsync(Guid id)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        return _mapper.Map<PaymentDto>(payment);
    }

    public async Task<List<PaymentDto>> GetAllPaymentsAsync()
    {
        var payments = await _paymentRepository.GetAllPaymentsAsync();
        return _mapper.Map<List<PaymentDto>>(payments);
    }

    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto paymentDto)
    {
        var payment = _mapper.Map<Payment>(paymentDto);
        var createdPayment = await _paymentRepository.CreatePaymentAsync(payment);
        return _mapper.Map<PaymentDto>(createdPayment);
    }

    public async Task UpdatePaymentAsync(Guid id, UpdatePaymentDto paymentDto)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if (payment == null) throw new KeyNotFoundException("Payment not found");

        _mapper.Map(paymentDto, payment);
        await _paymentRepository.UpdatePaymentAsync(payment);
    }

    public async Task DeletePaymentAsync(Guid id)
    {
        if (!await _paymentRepository.PaymentExistsAsync(id))
            throw new KeyNotFoundException("Payment not found");

        await _paymentRepository.DeletePaymentAsync(id);
    }

    public async Task<List<PaymentDto>> GetPaymentsByTeacherIdAsync(Guid teacherId)
    {
        var payments = await _paymentRepository.GetPaymentsByTeacherIdAsync(teacherId);
        return _mapper.Map<List<PaymentDto>>(payments);
    }

    public async Task<List<PaymentDto>> GetPaymentsByStudentIdAsync(Guid studentId)
    {
        var payments = await _paymentRepository.GetPaymentsByStudentIdAsync(studentId);
        return _mapper.Map<List<PaymentDto>>(payments);
    }
}