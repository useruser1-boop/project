// File: Customer.cs | Author: Team 05 | Course: ISTM 415
// Description: Model representing a lawn care customer with contact and billing information.
// On my honor, as an Aggie, I have neither given nor received unauthorized aid on this academic work.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;

/// <summary>
/// Represents a lawn care customer.
/// </summary>
public class Customer
{
    /// <summary>
    /// Gets or sets the customer identifier.
    /// </summary>
    public int CustomerID { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the billing street address.
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name = "Billing Address")]
    public string? BillingAddress { get; set; }

    /// <summary>
    /// Gets or sets the billing city.
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "City")]
    public string? BillingCity { get; set; }

    /// <summary>
    /// Gets or sets the billing state abbreviation.
    /// </summary>
    [Required]
    [StringLength(2)]
    [Display(Name = "State")]
    public string? BillingState { get; set; }

    /// <summary>
    /// Gets or sets the billing ZIP code (5 or 9 numeric digits).
    /// </summary>
    [Required]
    [StringLength(10)]
    [RegularExpression(@"^\d{5}(\d{4})?$", ErrorMessage = "ZIP must be exactly 5 or 9 numeric digits.")]
    [Display(Name = "ZIP")]
    public string? BillingZIP { get; set; }

    /// <summary>
    /// Gets or sets the customer phone number (10 numeric digits).
    /// </summary>
    [Required]
    [StringLength(20)]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be exactly 10 numeric digits.")]
    [Display(Name = "Phone")]
    public string? CustomerPhone { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string? Email { get; set; }

    /// <summary>
    /// Gets the full name.
    /// </summary>
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Gets or sets the customer's properties.
    /// </summary>
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
