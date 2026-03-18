// File: PropertySeed.cs | Author: Team ## | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds property records.
/// </summary>
public class PropertySeed : IEntityTypeConfiguration<Property>
{
    /// <summary>
    /// Configures property seed data.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasData(
            new Property { PropertyID = 1, CustomerID = 1, Address = "2101 Longmire Dr", City = "College Station", State = "TX", ZipCode = "77845", LotSize = 0.25m, ServiceFee = 120m },
            new Property { PropertyID = 2, CustomerID = 1, Address = "2205 Longmire Ct", City = "College Station", State = "TX", ZipCode = "77845", LotSize = 0.19m, ServiceFee = 105m },
            new Property { PropertyID = 3, CustomerID = 2, Address = "4203 Rock Prairie Rd", City = "College Station", State = "TX", ZipCode = "77845", LotSize = 0.30m, ServiceFee = 140m },
            new Property { PropertyID = 4, CustomerID = 2, Address = "1512 Birmingham Dr", City = "College Station", State = "TX", ZipCode = "77845", LotSize = 0.21m, ServiceFee = 110m },
            new Property { PropertyID = 5, CustomerID = 3, Address = "1708 Deacon Dr", City = "College Station", State = "TX", ZipCode = "77840", LotSize = 0.18m, ServiceFee = 98m },
            new Property { PropertyID = 6, CustomerID = 3, Address = "1801 Harvey Mitchell Pkwy S", City = "College Station", State = "TX", ZipCode = "77840", LotSize = 0.24m, ServiceFee = 125m },
            new Property { PropertyID = 7, CustomerID = 4, Address = "3315 Barron Rd", City = "College Station", State = "TX", ZipCode = "77845", LotSize = 0.26m, ServiceFee = 130m },
            new Property { PropertyID = 8, CustomerID = 4, Address = "3410 Finch Ln", City = "College Station", State = "TX", ZipCode = "77845", LotSize = 0.22m, ServiceFee = 115m },
            new Property { PropertyID = 9, CustomerID = 5, Address = "3910 Copperfield Dr", City = "Bryan", State = "TX", ZipCode = "77807", LotSize = 0.17m, ServiceFee = 95m },
            new Property { PropertyID = 10, CustomerID = 5, Address = "1820 Briarcrest Dr", City = "Bryan", State = "TX", ZipCode = "77802", LotSize = 0.20m, ServiceFee = 108m }
        );
    }
}
