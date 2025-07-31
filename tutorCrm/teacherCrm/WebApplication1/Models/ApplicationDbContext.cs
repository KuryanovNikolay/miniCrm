using Microsoft.EntityFrameworkCore;

namespace tutorCrm.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Пользователи и роли
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    // Учебные сущности
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Homework> Homeworks { get; set; }

    // Финансы
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUserAndRoles(modelBuilder);
        ConfigureEducationalEntities(modelBuilder);
        ConfigureFinancialEntities(modelBuilder);
        SeedInitialData(modelBuilder);
    }

    private void ConfigureUserAndRoles(ModelBuilder modelBuilder)
    {
        // Конфигурация связи многие-ко-многим между User и Role
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        // Уникальные поля
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();
    }

    private void ConfigureEducationalEntities(ModelBuilder modelBuilder)
    {
        // Конфигурация Lesson
        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Teacher)
            .WithMany()
            .HasForeignKey(l => l.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Student)
            .WithMany()
            .HasForeignKey(l => l.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Subject)
            .WithMany(s => s.Lessons)
            .HasForeignKey(l => l.SubjectId);

        // Ограничения для статусов уроков
        modelBuilder.Entity<Lesson>()
            .Property(l => l.Status)
            .HasDefaultValue("Scheduled")
            .HasConversion<string>();

        // Конфигурация Homework
        modelBuilder.Entity<Homework>()
            .HasOne(h => h.Lesson)
            .WithMany(l => l.Homeworks)
            .HasForeignKey(h => h.LessonId);

        // Ограничения для статусов домашних заданий
        modelBuilder.Entity<Homework>()
            .Property(h => h.Status)
            .HasDefaultValue("Assigned")
            .HasConversion<string>();
    }

    private void ConfigureFinancialEntities(ModelBuilder modelBuilder)
    {
        // Конфигурация Payment
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Teacher)
            .WithMany()
            .HasForeignKey(p => p.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Student)
            .WithMany()
            .HasForeignKey(p => p.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Lesson)
            .WithMany()
            .HasForeignKey(p => p.LessonId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Status)
            .HasDefaultValue("Pending")
            .HasConversion<string>();

        // Конфигурация Subscription
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Teacher)
            .WithMany()
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Student)
            .WithMany()
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Subject)
            .WithMany()
            .HasForeignKey(s => s.SubjectId);
    }

    private void SeedInitialData(ModelBuilder modelBuilder)
    {
        // Начальные роли
        var adminRoleId = Guid.NewGuid();
        var teacherRoleId = Guid.NewGuid();
        var studentRoleId = Guid.NewGuid();

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = adminRoleId, Name = "Admin", Description = "Администратор системы" },
            new Role { Id = teacherRoleId, Name = "Teacher", Description = "Преподаватель" },
            new Role { Id = studentRoleId, Name = "Student", Description = "Ученик" }
        );

        // Начальный администратор
        var adminUserId = Guid.NewGuid();
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminUserId,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Email = "admin@minicrm.com",
                FullName = "Администратор Системы",
                PhoneNumber = "+1234567890",
                ParentFullName = "N/A",
                RegistrationDate = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = adminUserId, RoleId = adminRoleId }
        );
    }
}

