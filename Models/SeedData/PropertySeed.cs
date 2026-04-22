// File: PropertySeed.cs | Author: Team 05 | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds 10 property records.
/// </summary>
public class PropertySeed : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasData(
            new Property { PropertyID = 1,  CustomerID = 1, PropertyAddress = "2101 Longmire Dr",              PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77845", ServiceFee = 120m },
            new Property { PropertyID = 2,  CustomerID = 1, PropertyAddress = "2205 Longmire Ct",              PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77845", ServiceFee = 105m },
            new Property { PropertyID = 3,  CustomerID = 2, PropertyAddress = "4203 Rock Prairie Rd",          PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77845", ServiceFee = 140m },
            new Property { PropertyID = 4,  CustomerID = 2, PropertyAddress = "1512 Birmingham Dr",            PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77845", ServiceFee = 110m },
            new Property { PropertyID = 5,  CustomerID = 3, PropertyAddress = "1708 Deacon Dr",                PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77840", ServiceFee = 98m  },
            new Property { PropertyID = 6,  CustomerID = 3, PropertyAddress = "1801 Harvey Mitchell Pkwy S",   PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77840", ServiceFee = 125m },
            new Property { PropertyID = 7,  CustomerID = 4, PropertyAddress = "3315 Barron Rd",                PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77845", ServiceFee = 130m },
            new Property { PropertyID = 8,  CustomerID = 4, PropertyAddress = "3410 Finch Ln",                 PropertyCity = "College Station", PropertyState = "TX", PropertyZIP = "77845", ServiceFee = 115m },
            new Property { PropertyID = 9,  CustomerID = 5, PropertyAddress = "3910 Copperfield Dr",           PropertyCity = "Bryan",           PropertyState = "TX", PropertyZIP = "77807", ServiceFee = 95m  },
            new Property { PropertyID = 10, CustomerID = 5, PropertyAddress = "1820 Briarcrest Dr",            PropertyCity = "Bryan",           PropertyState = "TX", PropertyZIP = "77802", ServiceFee = 108m }
        );
    }
}
