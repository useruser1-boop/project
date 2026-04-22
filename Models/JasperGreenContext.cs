// File: JasperGreenContext.cs | Author: Team 05 | Course: ISTM 415
using JasperGreen.Models.SeedData;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Models;

/// <summary>
/// EF Core DbContext for Jasper Green.
/// </summary>
public class JasperGreenContext(DbContextOptions<JasperGreenContext> options) : DbContext(options)
{
    /// <summary>Gets or sets customers.</summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>Gets or sets properties.</summary>
    public DbSet<Property> Properties => Set<Property>();

    /// <summary>Gets or sets employees.</summary>
    public DbSet<Employee> Employees => Set<Employee>();

    /// <summary>Gets or sets crews.</summary>
    public DbSet<Crew> Crews => Set<Crew>();

    /// <summary>Gets or sets service events.</summary>
    public DbSet<ProvideService> ProvideServices => Set<ProvideService>();

    /// <summary>Gets or sets payments.</summary>
    public DbSet<Payment> Payments => Set<Payment>();

    /// <summary>
    /// Configures model relationships and seed data.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Crew has three separate FK columns pointing to Employee — all use Restrict
        // so deleting an employee who is on a crew is prevented.
        modelBuilder.Entity<Crew>()
            .HasOne(c => c.CrewForeman)
            .WithMany()
            .HasForeignKey(c => c.CrewForemanID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Crew>()
            .HasOne(c => c.CrewMember1)
            .WithMany()
            .HasForeignKey(c => c.CrewMember1ID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Crew>()
            .HasOne(c => c.CrewMember2)
            .WithMany()
            .HasForeignKey(c => c.CrewMember2ID)
            .OnDelete(DeleteBehavior.Restrict);

        // ProvideService FKs — use Restrict to avoid multiple cascade paths
        modelBuilder.Entity<ProvideService>()
            .HasOne(ps => ps.Customer)
            .WithMany()
            .HasForeignKey(ps => ps.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProvideService>()
            .HasOne(ps => ps.Property)
            .WithMany(p => p.ProvideServices)
            .HasForeignKey(ps => ps.PropertyID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProvideService>()
            .HasOne(ps => ps.Crew)
            .WithMany(c => c.ProvideServices)
            .HasForeignKey(ps => ps.CrewID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProvideService>()
            .HasOne(ps => ps.Payment)
            .WithMany()
            .HasForeignKey(ps => ps.PaymentID)
            .OnDelete(DeleteBehavior.SetNull);

        // Payment belongs to a Customer (many payments per customer).
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Customer)
            .WithMany()
            .HasForeignKey(p => p.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);

        // Apply seed data configurations.
        modelBuilder.ApplyConfiguration(new CustomerSeed());
        modelBuilder.ApplyConfiguration(new PropertySeed());
        modelBuilder.ApplyConfiguration(new EmployeeSeed());
        modelBuilder.ApplyConfiguration(new CrewSeed());
        modelBuilder.ApplyConfiguration(new ProvideServiceSeed());
        modelBuilder.ApplyConfiguration(new PaymentSeed());
    }
}
