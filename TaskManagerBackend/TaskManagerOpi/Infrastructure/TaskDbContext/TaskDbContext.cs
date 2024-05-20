using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TaskDbContext
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "felipe123",
                    Password = "12345678",
                    Email = "correo@example.com",
                    NotificationPreference = NotificationPreference.OneDay 
                }
            );
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Id);

                entity.Property(user => user.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(user => user.Password)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(user => user.Email)
                    .IsRequired();

                entity.Property(user => user.NotificationPreference)
                    .IsRequired();

                entity.HasMany(user => user.Tasks)
                    .WithOne(task => task.User)
                    .HasForeignKey(task => task.UserId);
            });

            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.HasKey(task => task.Id);

                entity.Property(task => task.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(task => task.Status)
                    .IsRequired();

                entity.Property(task => task.Importance)
                    .IsRequired();

                entity.Property(task => task.ExpirationDate)
                    .IsRequired();

                entity.Property(task => task.Details)
                    .HasMaxLength(200);
                entity.HasOne(task => task.User)
                    .WithMany(user => user.Tasks);
            });
        }
    }
}
