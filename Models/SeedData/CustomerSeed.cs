// File: CustomerSeed.cs | Author: Team 05 | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds 5 customer records.
/// </summary>
public class CustomerSeed : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(
            new Customer { CustomerID = 1, FirstName = "Maria",    LastName = "Garcia",  CustomerPhone = "9795551122", Email = "maria.garcia@email.com", BillingAddress = "2101 Longmire Dr",       BillingCity = "College Station", BillingState = "TX", BillingZIP = "77845" },
            new Customer { CustomerID = 2, FirstName = "Jane",    LastName = "Doe",  CustomerPhone = "9795552233", Email = "janedoe@tamu.com",      BillingAddress = "4203 Rock Prairie Rd",   BillingCity = "College Station", BillingState = "TX", BillingZIP = "77845" },
            new Customer { CustomerID = 3, FirstName = "Queen", LastName = "Rev      ",    CustomerPhone = "9795553344", Email = "rev@example.com",         BillingAddress = "503 George Bush Dr",         BillingCity = "College Station", BillingState = "TX", BillingZIP = "77840" },
            new Customer { CustomerID = 4, FirstName = "John",    LastName = "Doe",  CustomerPhone = "9795554455", Email = "johndoe@example.com",       BillingAddress = "3315 Barron Rd",         BillingCity = "College Station", BillingState = "TX", BillingZIP = "77845" },
            new Customer { CustomerID = 5, FirstName = "Alyssa",   LastName = "Jones",   CustomerPhone = "9795555566", Email = "ajones@example.com",        BillingAddress = "805 Southwest Pkwy E",   BillingCity = "College Station", BillingState = "TX", BillingZIP = "77840" }
        );
    }
}
