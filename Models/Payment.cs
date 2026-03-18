// File: Payment.cs | Author: Team ## | Course: ISTM 415
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;

/// <summary>
/// Represents a payment for a completed service event.
/// </summary>
public class Payment
{
    /// <summary>
    /// Gets or sets the payment identifier.
    /// </summary>
    public int PaymentID { get; set; }

    /// <summary>
    /// Gets or sets the linked service event identifier.
    /// </summary>
    [Required]
    public int ProvideServiceID { get; set; }

    /// <summary>
    /// Gets or sets the payment date.
    /// </summary>
    [Required]
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the amount paid.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal AmountPaid { get; set; }

    /// <summary>
    /// Gets or sets the related service event.
    /// </summary>
    public ProvideService? ProvideService { get; set; }
}
