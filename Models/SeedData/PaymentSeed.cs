// File: PaymentSeed.cs | Author: Team 05 | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds 5 payment records.
/// </summary>
public class PaymentSeed : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasData(
            new Payment { PaymentID = 1, CustomerID = 1, PaymentDate = new DateTime(2025, 1,  7),  PaymentAmount = 125m },
            new Payment { PaymentID = 2, CustomerID = 1, PaymentDate = new DateTime(2025, 1, 14), PaymentAmount = 110m },
            new Payment { PaymentID = 3, CustomerID = 2, PaymentDate = new DateTime(2025, 1, 21), PaymentAmount = 145m },
            new Payment { PaymentID = 4, CustomerID = 2, PaymentDate = new DateTime(2025, 1, 28), PaymentAmount = 115m },
            new Payment { PaymentID = 5, CustomerID = 3, PaymentDate = new DateTime(2025, 2,  6),  PaymentAmount = 100m }
        );
    }
}
