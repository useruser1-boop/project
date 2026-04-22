// File: ProvideService.cs | Author: Team 05 | Course: ISTM 415
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;

/// <summary>
/// Represents a completed lawn service event.
/// </summary>
public class ProvideService
{
    /// <summary>
    /// Gets or sets the service event identifier.
    /// </summary>
    [Key]
    public int ServiceID { get; set; }

    /// <summary>
    /// Gets or sets the crew identifier.
    /// </summary>
    [Required]
    public int CrewID { get; set; }

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
    /// Gets or sets the date the service was performed.
    /// </summary>
    [Required]
    [Display(Name = "Service Date")]
    public DateTime ServiceDate { get; set; }

    /// <summary>
    /// Gets or sets the fee charged for this service visit.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Service Fee")]
    public decimal ServiceFee { get; set; }

    /// <summary>
    /// Gets or sets the optional payment identifier (null = unpaid).
    /// </summary>
    public int? PaymentID { get; set; }

    /// <summary>
    /// Gets or sets the customer navigation property.
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Gets or sets the property navigation property.
    /// </summary>
    public Property? Property { get; set; }

    /// <summary>
    /// Gets or sets the crew navigation property.
    /// </summary>
    public Crew? Crew { get; set; }

    /// <summary>
    /// Gets or sets the optional payment navigation property.
    /// </summary>
    public Payment? Payment { get; set; }
}
