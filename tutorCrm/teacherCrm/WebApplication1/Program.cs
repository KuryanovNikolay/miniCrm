using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;
using WebApplication1.Profiles;
using WebApplication1.Repositories;
using WebApplication1.Repositories.HomeworkRepositories;
using WebApplication1.Repositories.RoleRepositories;
using WebApplication1.Repositories.SubjectRepositories; // ��������� using ��� ������������ Subject
using WebApplication1.Services;
using WebApplication1.Services.HomeworkServices;
using WebApplication1.Services.RoleServices;
using WebApplication1.Services.SubjectServices; // ��������� using ��� �������� Subject

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