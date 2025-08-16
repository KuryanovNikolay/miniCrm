using AutoMapper;
using tutorCrm.Models;
using WebApplication1.Dtos.PaymentDtos;
using WebApplication1.Repositories.PaymentRepositories;

namespace WebApplication1.Services.PaymentServices;

/// <summary>
/// Сервис для работы с платежами.
/// Реализует бизнес-логику управления платежами.
/// </summary>
public class PaymentService : IPaymentService
{
    /// <summary>
    /// Репозиторий для работы с платежами в базе данных.
    /// </summary>
    private readonly IPaymentRepository _paymentRepository;

    /// <summary>
    /// Объект для автоматического маппинга между DTO и моделями.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса платежей.
    /// </summary>
    /// <param name="paymentRepository">Репозиторий платежей.</param>
    /// <param name="mapper">Объект для преобразования моделей.</param>
    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает платеж по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор платежа.</param>
    /// <returns>DTO объекта платежа.</returns>
    /// <exception cref="KeyNotFoundException">Если платеж не найден.</exception>
    public async Task<PaymentDto> GetPaymentByIdAsync(Guid id)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        return _mapper.Map<PaymentDto>(payment);
    }

    /// <summary>
    /// Получает список всех платежей в системе.
    /// </summary>
    /// <returns>Список DTO объектов платежей.</returns>
    public async Task<List<PaymentDto>> GetAllPaymentsAsync()
    {
        var payments = await _paymentRepository.GetAllPaymentsAsync();
        return _mapper.Map<List<PaymentDto>>(payments);
    }

    /// <summary>
    /// Создает новый платеж в системе.
    /// </summary>
    /// <param name="paymentDto">DTO с данными для создания платежа.</param>
    /// <returns>DTO созданного платежа.</returns>
    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto paymentDto)
    {
        var payment = _mapper.Map<Payment>(paymentDto);
        var createdPayment = await _paymentRepository.CreatePaymentAsync(payment);
        return _mapper.Map<PaymentDto>(createdPayment);
    }

    /// <summary>
    /// Обновляет существующий платеж.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого платежа.</param>
    /// <param name="paymentDto">DTO с обновленными данными платежа.</param>
    /// <exception cref="KeyNotFoundException">Если платеж не найден.</exception>
    public async Task UpdatePaymentAsync(Guid id, UpdatePaymentDto paymentDto)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if (payment == null) throw new KeyNotFoundException("Payment not found");

        _mapper.Map(paymentDto, payment);
        await _paymentRepository.UpdatePaymentAsync(payment);
    }

    /// <summary>
    /// Удаляет платеж по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого платежа.</param>
    /// <exception cref="KeyNotFoundException">Если платеж не найден.</exception>
    public async Task DeletePaymentAsync(Guid id)
    {
        if (!await _paymentRepository.PaymentExistsAsync(id))
            throw new KeyNotFoundException("Payment not found");

        await _paymentRepository.DeletePaymentAsync(id);
    }

    /// <summary>
    /// Получает платежи по идентификатору преподавателя.
    /// </summary>
    /// <param name="teacherId">Идентификатор преподавателя.</param>
    /// <returns>Список DTO платежей преподавателя.</returns>
    public async Task<List<PaymentDto>> GetPaymentsByTeacherIdAsync(Guid teacherId)
    {
        var payments = await _paymentRepository.GetPaymentsByTeacherIdAsync(teacherId);
        return _mapper.Map<List<PaymentDto>>(payments);
    }

    /// <summary>
    /// Получает платежи по идентификатору студента.
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <returns>Список DTO платежей студента.</returns>
    public async Task<List<PaymentDto>> GetPaymentsByStudentIdAsync(Guid studentId)
    {
        var payments = await _paymentRepository.GetPaymentsByStudentIdAsync(studentId);
        return _mapper.Map<List<PaymentDto>>(payments);
    }
}