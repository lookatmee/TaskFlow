namespace TaskManager.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.HasOne(e => e.AssignedUser)
                  .WithMany(u => u.AssignedWorkItems)
                  .HasForeignKey(e => e.AssignedUserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();

            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasOne(e => e.Role)
                  .WithMany(r => r.Users)
                  .HasForeignKey(e => e.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
        });

        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role 
            { 
                Id = 1, 
                Name = "Administrator", 
                Description = "System Administrator with full access" 
            },
            new Role 
            { 
                Id = 2, 
                Name = "User", 
                Description = "Regular system user" 
            }
        );

        // Seed Admin User
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = 1,
                Name = "Adolfo",
                Email = "adolfojinete@hotmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("qwerty789"),
                RoleId = 1 // Administrator Role
            }
        );
    }
} 