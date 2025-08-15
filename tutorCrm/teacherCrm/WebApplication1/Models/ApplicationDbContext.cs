using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tutorCrm.Models;

namespace tutorCrm.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Homework> Homeworks { get; set; }

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);
            b.Property(u => u.EmailConfirmed).HasDefaultValue(false);
            b.Property(u => u.PasswordHash);
            b.Property(u => u.SecurityStamp);
            b.Property(u => u.ConcurrencyStamp);
            b.Property(u => u.PhoneNumber).HasMaxLength(20);
            b.Property(u => u.PhoneNumberConfirmed).HasDefaultValue(false);
            b.Property(u => u.TwoFactorEnabled).HasDefaultValue(false);
            b.Property(u => u.LockoutEnd);
            b.Property(u => u.LockoutEnabled).HasDefaultValue(false);
            b.Property(u => u.AccessFailedCount).HasDefaultValue(0);

            b.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(u => u.RegistrationDate)
                .HasDefaultValueSql("GETUTCDATE()");

            b.Property(u => u.ParentFullName)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(u => u.ParentContact)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<IdentityRole<Guid>>(b =>
        {
            b.Property(r => r.Name).HasMaxLength(256);
            b.Property(r => r.NormalizedName).HasMaxLength(256);
        });

        ConfigureEducationalEntities(modelBuilder);
        ConfigureFinancialEntities(modelBuilder);

        SeedInitialData(modelBuilder);
    }

    private void ConfigureEducationalEntities(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<Lesson>()
            .Property(l => l.Status)
            .HasDefaultValue("Scheduled")
            .HasConversion<string>();

        modelBuilder.Entity<Homework>()
            .HasOne(h => h.Lesson)
            .WithMany(l => l.Homeworks)
            .HasForeignKey(h => h.LessonId);

        modelBuilder.Entity<Homework>()
            .Property(h => h.Status)
            .HasDefaultValue("Assigned")
            .HasConversion<string>();
    }

    private void ConfigureFinancialEntities(ModelBuilder modelBuilder)
    {
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
        var adminRoleId = Guid.NewGuid();
        var teacherRoleId = Guid.NewGuid();
        var studentRoleId = Guid.NewGuid();

        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole<Guid>
            {
                Id = teacherRoleId,
                Name = "Teacher",
                NormalizedName = "TEACHER"
            },
            new IdentityRole<Guid>
            {
                Id = studentRoleId,
                Name = "Student",
                NormalizedName = "STUDENT"
            }
        );

        var adminUserId = Guid.NewGuid();
        var hasher = new PasswordHasher<ApplicationUser>();

        var adminUser = new ApplicationUser
        {
            Id = adminUserId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@tutorcrm.com",
            NormalizedEmail = "ADMIN@TUTORCRM.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Admin123!"),
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PhoneNumber = "+1234567890",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0,
            FullName = "Системный Администратор",
            ParentFullName = "N/A",
            RegistrationDate = DateTime.UtcNow
        };

        modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                UserId = adminUserId,
                RoleId = adminRoleId
            }
        );
    }
}