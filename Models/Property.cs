// File: Property.cs | Author: Team 05 | Course: ISTM 415
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;   

/// <summary>
/// Represents a service property belonging to a customer.
/// </summary>
public class Property
{
    /// <summary>
    /// Gets or sets the property identifier.
    /// </summary>
    public int PropertyID { get; set; }

    /// <summary>
    /// Gets or sets the owning customer identifier.
    /// </summary>
    [Required]
    public int CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the property street address.     
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Address")]
    public string PropertyAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the property city.
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "City")]
    public string PropertyCity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the property state abbreviation.
    /// </summary>
    [Required]
    [StringLength(2)]
    [Display(Name = "State")]
    public string PropertyState { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the property ZIP code.
    /// </summary>
    [Required]
    [StringLength(10)]
    [Display(Name = "ZIP")]
    public string PropertyZIP { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the contracted monthly service fee.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Service Fee")]
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
 