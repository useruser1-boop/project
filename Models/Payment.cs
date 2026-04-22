// File: Payment.cs | Author: Team 05 | Course: ISTM 415
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;  

/// <summary>
/// Represents a customer payment.
/// </summary>
public class Payment
{
    /// <summary>
    /// Gets or sets the payment identifier.
    /// </summary>
    public int PaymentID { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    [Required]
    public int CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the payment date.
    /// </summary>
    [Required]
    [Display(Name = "Payment Date")]
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the amount paid.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Payment Amount")]
    public decimal PaymentAmount { get; set; }

    /// <summary>
    /// Gets or sets the customer navigation property.
    /// </summary>
    public Customer? Customer { get; set; }
}
