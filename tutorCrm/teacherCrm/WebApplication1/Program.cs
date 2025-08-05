using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;
using WebApplication1.Profiles;
using WebApplication1.Repositories;
using WebApplication1.Repositories.HomeworkRepositories;
using WebApplication1.Repositories.LessonRepositories;
using WebApplication1.Repositories.PaymentRepositories;
using WebApplication1.Repositories.RoleRepositories;
using WebApplication1.Repositories.SubjectRepositories; // ��������� using ��� ������������ Subject
using WebApplication1.Repositories.SubscriptionRepositories;
using WebApplication1.Services;
using WebApplication1.Services.HomeworkServices;
using WebApplication1.Services.LessonServices;
using WebApplication1.Services.PaymentServices;
using WebApplication1.Services.RoleServices;
using WebApplication1.Services.SubjectServices;
using WebApplication1.Services.SubscriptionServices; // ��������� using ��� �������� Subject

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ������������ ����������� � �������
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

// ��������� ����������� ��� Subject
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

// ����������� ������������ � ��������
builder.Services.AddScoped<IHomeworkRepository, HomeworkRepository>();
builder.Services.AddScoped<IHomeworkService, HomeworkService>();

builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

// ����������� AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile)); // ��������� ���������� �������

// ��� �������������� �������, ���� ����������� ������������ ������:
// builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();