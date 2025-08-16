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

/// <summary>
/// Точка входа приложения Tutor CRM.
/// </summary>
public class Program
{
    /// <summary>
    /// Основной метод приложения.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);
        ConfigureIdentity(builder);
        ConfigureAuthorization(builder);
        ConfigureSwagger(builder);

        var app = builder.Build();

        ConfigureMiddleware(app);
        await InitializeDatabase(app);

        await app.RunAsync();
    }

    /// <summary>
    /// Конфигурирует сервисы приложения.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        ConfigureDatabase(builder);
        RegisterApplicationServices(builder);
        ConfigureAutoMapper(builder);
    }

    /// <summary>
    /// Конфигурирует подключение к базе данных.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    }

    /// <summary>
    /// Регистрирует сервисы приложения.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    private static void RegisterApplicationServices(WebApplicationBuilder builder)
    {
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
        builder.Services.AddScoped<IUserService, UserService>();
    }

    /// <summary>
    /// Конфигурирует AutoMapper.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    private static void ConfigureAutoMapper(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }

    /// <summary>
    /// Конфигурирует систему идентификации.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    private static void ConfigureIdentity(WebApplicationBuilder builder)
    {
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

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/account/login";
            options.AccessDeniedPath = "/account/access-denied";
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.SlidingExpiration = true;
        });
    }

    /// <summary>
    /// Конфигурирует политики авторизации.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    private static void ConfigureAuthorization(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireTeacherRole", policy => policy.RequireRole("Teacher"));
        });
    }

    /// <summary>
    /// Конфигурирует Swagger для API документации.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Tutor CRM API", Version = "v1" });
        });
    }

    /// <summary>
    /// Конфигурирует middleware приложения.
    /// </summary>
    /// <param name="app">Построенное приложение.</param>
    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tutor CRM API v1"));
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapRazorPages();
    }

    /// <summary>
    /// Инициализирует базу данных и начальные данные.
    /// </summary>
    /// <param name="app">Построенное приложение.</param>
    private static async Task InitializeDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            await context.Database.MigrateAsync();
            await SeedInitialData(userManager, roleManager);
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }

    /// <summary>
    /// Заполняет базу данных начальными данными.
    /// </summary>
    /// <param name="userManager">Менеджер пользователей.</param>
    /// <param name="roleManager">Менеджер ролей.</param>
    private static async Task SeedInitialData(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        await CreateRoles(roleManager);
        await CreateAdminUser(userManager);
    }

    /// <summary>
    /// Создает стандартные роли в системе.
    /// </summary>
    /// <param name="roleManager">Менеджер ролей.</param>
    private static async Task CreateRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roleNames = { "Admin", "Teacher", "Student" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }
    }

    /// <summary>
    /// Создает администратора системы.
    /// </summary>
    /// <param name="userManager">Менеджер пользователей.</param>
    private static async Task CreateAdminUser(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@tutorcrm.com";
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
}