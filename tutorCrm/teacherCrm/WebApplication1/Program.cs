using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tutorCrm.Data;
using tutorCrm.Models;
using WebApplication1.Profiles;
using WebApplication1.Repositories;
using WebApplication1.Repositories.HomeworkRepositories;
using WebApplication1.Repositories.LessonRepositories;
using WebApplication1.Repositories.PaymentRepositories;
using WebApplication1.Repositories.SubjectRepositories;
using WebApplication1.Repositories.SubscriptionRepositories;
using WebApplication1.Services;
using WebApplication1.Services.HomeworkServices;
using WebApplication1.Services.LessonServices;
using WebApplication1.Services.PaymentServices;
using WebApplication1.Services.SubjectServices;
using WebApplication1.Services.SubscriptionServices;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация базы данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Настройка Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Регистрация сервисов
builder.Services.AddScoped<IHomeworkRepository, HomeworkRepository>();
builder.Services.AddScoped<IHomeworkService, HomeworkService>();

builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
// Регистрация сервисов
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHomeworkService, HomeworkService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Tutor CRM API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new() { /* конфигурация JWT */ });
    c.AddSecurityRequirement(new() { /* конфигурация безопасности */ });
});

// Аутентификация и авторизация
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireTeacherRole", policy => policy.RequireRole("Teacher"));
});

var app = builder.Build();

// Конфигурация middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tutor CRM API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Инициализация базы данных
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await context.Database.MigrateAsync();

        // Инициализация начальных данных
        await InitializeSeedData(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();

async Task InitializeSeedData(ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<Guid>> roleManager)
{
    // Создание ролей
    string[] roleNames = { "Admin", "Teacher", "Student" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }

    // Создание администратора
    var adminEmail = "admin@tutorcrm.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new ApplicationUser
        {
            UserName = "admin",
            Email = adminEmail,
            FullName = "System Administrator",
            ParentFullName = "N/A",
            RegistrationDate = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(adminUser, "Admin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}