// File: CustomerSeed.cs | Author: Team ## | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds customer records.
/// </summary>
public class CustomerSeed : IEntityTypeConfiguration<Customer>
{
    /// <summary>
    /// Configures customer seed data.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(
            new Customer { CustomerID = 1, FirstName = "Maria", LastName = "Garcia", Phone = "9795551122", Email = "maria.garcia@example.com", Address = "2101 Longmire Dr", City = "College Station", State = "TX", ZipCode = "77845" },
            new Customer { CustomerID = 2, FirstName = "Derek", LastName = "Nguyen", Phone = "9795552233", Email = "d.nguyen@example.com", Address = "4203 Rock Prairie Rd", City = "College Station", State = "TX", ZipCode = "77845" },
            new Customer { CustomerID = 3, FirstName = "Samantha", LastName = "Reed", Phone = "9795553344", Email = "sreed@example.com", Address = "1708 Deacon Dr", City = "College Station", State = "TX", ZipCode = "77840" },
            new Customer { CustomerID = 4, FirstName = "Tyler", LastName = "Hughes", Phone = "9795554455", Email = "thughes@example.com", Address = "3315 Barron Rd", City = "College Station", State = "TX", ZipCode = "77845" },
            new Customer { CustomerID = 5, FirstName = "Alyssa", LastName = "Patel", Phone = "9795555566", Email = "apatel@example.com", Address = "805 Southwest Pkwy E", City = "College Station", State = "TX", ZipCode = "77840" }
        );
    }
}
