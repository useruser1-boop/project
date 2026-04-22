// File: ProvideServiceSeed.cs | Author: Team 05 | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds 10 service event records.
/// </summary>
public class ProvideServiceSeed : IEntityTypeConfiguration<ProvideService>
{
    public void Configure(EntityTypeBuilder<ProvideService> builder)
    {
        // ServiceFee charged is always >= the contracted property ServiceFee.
        builder.HasData(
            new ProvideService { ServiceID = 1,  CrewID = 1, CustomerID = 1, PropertyID = 1,  ServiceDate = new DateTime(2025, 1,  5),  ServiceFee = 125m, PaymentID = 1 },
            new ProvideService { ServiceID = 2,  CrewID = 3, CustomerID = 1, PropertyID = 2,  ServiceDate = new DateTime(2025, 1, 12), ServiceFee = 110m, PaymentID = 2 },
            new ProvideService { ServiceID = 3,  CrewID = 2, CustomerID = 2, PropertyID = 3,  ServiceDate = new DateTime(2025, 1, 18), ServiceFee = 145m, PaymentID = 3 },
            new ProvideService { ServiceID = 4,  CrewID = 4, CustomerID = 2, PropertyID = 4,  ServiceDate = new DateTime(2025, 1, 25), ServiceFee = 115m, PaymentID = 4 },
            new ProvideService { ServiceID = 5,  CrewID = 1, CustomerID = 3, PropertyID = 5,  ServiceDate = new DateTime(2025, 2,  2),  ServiceFee = 100m, PaymentID = 5 },
            new ProvideService { ServiceID = 6,  CrewID = 5, CustomerID = 3, PropertyID = 6,  ServiceDate = new DateTime(2025, 2, 10), ServiceFee = 128m },
            new ProvideService { ServiceID = 7,  CrewID = 2, CustomerID = 4, PropertyID = 7,  ServiceDate = new DateTime(2025, 2, 18), ServiceFee = 132m },
            new ProvideService { ServiceID = 8,  CrewID = 4, CustomerID = 4, PropertyID = 8,  ServiceDate = new DateTime(2025, 2, 26), ServiceFee = 118m },
            new ProvideService { ServiceID = 9,  CrewID = 5, CustomerID = 5, PropertyID = 9,  ServiceDate = new DateTime(2025, 3,  7),  ServiceFee = 98m  },
            new ProvideService { ServiceID = 10, CrewID = 3, CustomerID = 5, PropertyID = 10, ServiceDate = new DateTime(2025, 3, 15), ServiceFee = 112m }
        );
    }
}
