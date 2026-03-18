// File: ProvideServiceSeed.cs | Author: Team ## | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds service event records.
/// </summary>
public class ProvideServiceSeed : IEntityTypeConfiguration<ProvideService>
{
    /// <summary>
    /// Configures service event seed data.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<ProvideService> builder)
    {
        // ServiceFeeCharged is always >= the contracted property ServiceFee.
        builder.HasData(
            new ProvideService { ProvideServiceID = 1, CustomerID = 1, PropertyID = 1, CrewID = 1, ServiceDate = new DateTime(2025, 1, 5), ServiceFeeCharged = 125m, PaymentID = 1 },
            new ProvideService { ProvideServiceID = 2, CustomerID = 1, PropertyID = 2, CrewID = 3, ServiceDate = new DateTime(2025, 1, 12), ServiceFeeCharged = 110m, PaymentID = 2 },
            new ProvideService { ProvideServiceID = 3, CustomerID = 2, PropertyID = 3, CrewID = 2, ServiceDate = new DateTime(2025, 1, 18), ServiceFeeCharged = 145m, PaymentID = 3 },
            new ProvideService { ProvideServiceID = 4, CustomerID = 2, PropertyID = 4, CrewID = 4, ServiceDate = new DateTime(2025, 1, 25), ServiceFeeCharged = 115m, PaymentID = 4 },
            new ProvideService { ProvideServiceID = 5, CustomerID = 3, PropertyID = 5, CrewID = 1, ServiceDate = new DateTime(2025, 2, 2), ServiceFeeCharged = 100m, PaymentID = 5 },
            new ProvideService { ProvideServiceID = 6, CustomerID = 3, PropertyID = 6, CrewID = 5, ServiceDate = new DateTime(2025, 2, 10), ServiceFeeCharged = 128m },
            new ProvideService { ProvideServiceID = 7, CustomerID = 4, PropertyID = 7, CrewID = 2, ServiceDate = new DateTime(2025, 2, 18), ServiceFeeCharged = 132m },
            new ProvideService { ProvideServiceID = 8, CustomerID = 4, PropertyID = 8, CrewID = 4, ServiceDate = new DateTime(2025, 2, 26), ServiceFeeCharged = 118m },
            new ProvideService { ProvideServiceID = 9, CustomerID = 5, PropertyID = 9, CrewID = 5, ServiceDate = new DateTime(2025, 3, 7), ServiceFeeCharged = 98m },
            new ProvideService { ProvideServiceID = 10, CustomerID = 5, PropertyID = 10, CrewID = 3, ServiceDate = new DateTime(2025, 3, 15), ServiceFeeCharged = 112m }
        );
    }
}
