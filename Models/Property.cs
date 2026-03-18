// File: Property.cs | Author: Team ## | Course: ISTM 415
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;

/// <summary>
/// Represents a service property for a customer.
/// </summary>
public class Property
{
    /// <summary>
    /// Gets or sets the property identifier.
    /// </summary>
    public int PropertyID { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    [Required]
    public int CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state abbreviation.
    /// </summary>
    [Required]
    [StringLength(2)]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ZIP code.
    /// </summary>
    [Required]
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the lot size.
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? LotSize { get; set; }

    /// <summary>
    /// Gets or sets the contracted monthly service fee.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ServiceFee { get; set; }

    /// <summary>
    /// Gets or sets the owning customer.
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Gets or sets related service events.
    /// </summary>
    public ICollection<ProvideService> ProvideServices { get; set; } = new List<ProvideService>();
}
