using CostMSWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CostMSWebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Funds> Funds { get; set; }
    public DbSet<Indicator> Indicators { get; set; }
    public DbSet<Meter> Meters { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne(category => category.User)
            .WithMany(user => user.Categories)
            .HasForeignKey(category => category.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Funds>()
            .HasOne(funds => funds.User)
            .WithMany(user => user.Funds)
            .HasForeignKey(funds => funds.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Indicator>()
            .HasOne(indicator => indicator.Meter)
            .WithMany(meter => meter.Indicators)
            .HasForeignKey(indicator => indicator.MeterId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Indicator>()
            .HasOne(indicator => indicator.Tariff)
            .WithMany(tariff => tariff.Indicators)
            .HasForeignKey(indicator => indicator.TariffId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Indicator>()
            .HasOne(indicator => indicator.Previous)
            .WithOne()
            .HasForeignKey<Indicator>(indicator => indicator.PreviousId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Meter>()
            .HasOne(meter => meter.Category)
            .WithMany(category => category.Meters)
            .HasForeignKey(meter => meter.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Meter>()
            .HasOne(meter => meter.Unit)
            .WithMany(unit => unit.Meters)
            .HasForeignKey(meter => meter.UnitId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Tariff>()
            .HasOne(tariff => tariff.Category)
            .WithMany(category => category.Tariffs)
            .HasForeignKey(tariff => tariff.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tariff>()
            .HasOne(tariff => tariff.Unit)
            .WithMany(unit => unit.Tariffs)
            .HasForeignKey(tariff => tariff.UnitId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(user => user.Authorities)
            .WithMany(role => role.Users)
            .UsingEntity("user_role",
                l => l.HasOne(typeof(Role))
                    .WithMany()
                    .HasForeignKey("role_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(Role.Id)),
                r => r.HasOne(typeof(User))
                    .WithMany()
                    .HasForeignKey("user_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(User.Id)),
                j => j.HasKey("user_id", "role_id"));
    }
}
