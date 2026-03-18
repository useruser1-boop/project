// File: ProvideService.cs | Author: Team ## | Course: ISTM 415
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;

/// <summary>
/// Represents a completed service event.
/// </summary>
public class ProvideService
{
    /// <summary>
    /// Gets or sets the service event identifier.
    /// </summary>
    public int ProvideServiceID { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    [Required]
    public int CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the property identifier.
    /// </summary>
    [Required]
    public int PropertyID { get; set; }

    /// <summary>
    /// Gets or sets the crew identifier.
    /// </summary>
    [Required]
    public int CrewID { get; set; }

    /// <summary>
    /// Gets or sets the service date.
    /// </summary>
    [Required]
    public DateTime ServiceDate { get; set; }

    /// <summary>
    /// Gets or sets the service fee charged.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ServiceFeeCharged { get; set; }

    /// <summary>
    /// Gets or sets the optional payment identifier.
    /// </summary>
    public int? PaymentID { get; set; }

    /// <summary>
    /// Gets or sets the customer.
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Gets or sets the property.
    /// </summary>
    public Property? Property { get; set; }

    /// <summary>
    /// Gets or sets the crew.
    /// </summary>
    public Crew? Crew { get; set; }

    /// <summary>
    /// Gets or sets the optional payment.
    /// </summary>
    public Payment? Payment { get; set; }
}
