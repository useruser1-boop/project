// File: JasperGreenContext.cs | Author: Team ## | Course: ISTM 415
using JasperGreen.Models.SeedData;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Models;

/// <summary>
/// EF Core DbContext for Jasper Green.
/// </summary>
public class JasperGreenContext(DbContextOptions<JasperGreenContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets customers.
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>
    /// Gets or sets properties.
    /// </summary>
    public DbSet<Property> Properties => Set<Property>();

    /// <summary>
    /// Gets or sets employees.
    /// </summary>
    public DbSet<Employee> Employees => Set<Employee>();

    /// <summary>
    /// Gets or sets crews.
    /// </summary>
    public DbSet<Crew> Crews => Set<Crew>();

    /// <summary>
    /// Gets or sets service events.
    /// </summary>
    public DbSet<ProvideService> ProvideServices => Set<ProvideService>();

    /// <summary>
    /// Gets or sets payments.
    /// </summary>
    public DbSet<Payment> Payments => Set<Payment>();

    /// <summary>
    /// Configures model relationships and seed data.
    /// </summary>
    /// <param name="modelBuilder">Model builder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure three separate employee foreign keys for crew members and foreman.
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

        // Configure service/payment links explicitly because both entities contain
        // navigation properties and foreign keys that conventions cannot disambiguate.
        modelBuilder.Entity<ProvideService>()
            .HasOne(ps => ps.Payment)
            .WithMany()
            .HasForeignKey(ps => ps.PaymentID)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.ProvideService)
            .WithMany()
            .HasForeignKey(p => p.ProvideServiceID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.ApplyConfiguration(new CustomerSeed());
        modelBuilder.ApplyConfiguration(new PropertySeed());
        modelBuilder.ApplyConfiguration(new EmployeeSeed());
        modelBuilder.ApplyConfiguration(new CrewSeed());
        modelBuilder.ApplyConfiguration(new ProvideServiceSeed());
        modelBuilder.ApplyConfiguration(new PaymentSeed());
    }
}
