// File: PaymentSeed.cs | Author: Team ## | Course: ISTM 415
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JasperGreen.Models.SeedData;

/// <summary>
/// Seeds payment records.
/// </summary>
public class PaymentSeed : IEntityTypeConfiguration<Payment>
{
    /// <summary>
    /// Configures payment seed data.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasData(
            new Payment { PaymentID = 1, ProvideServiceID = 1, PaymentDate = new DateTime(2025, 1, 7), AmountPaid = 125m },
            new Payment { PaymentID = 2, ProvideServiceID = 2, PaymentDate = new DateTime(2025, 1, 14), AmountPaid = 110m },
            new Payment { PaymentID = 3, ProvideServiceID = 3, PaymentDate = new DateTime(2025, 1, 21), AmountPaid = 145m },
            new Payment { PaymentID = 4, ProvideServiceID = 4, PaymentDate = new DateTime(2025, 1, 28), AmountPaid = 115m },
            new Payment { PaymentID = 5, ProvideServiceID = 5, PaymentDate = new DateTime(2025, 2, 6), AmountPaid = 100m }
        );
    }
}
