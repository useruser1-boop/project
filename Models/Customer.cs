// File: Customer.cs | Author: Team 05 | Course: ISTM 415
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
    [StringLength(100)]
    [Display(Name = "Billing Address")]
    public string? BillingAddress { get; set; }

    /// <summary>
    /// Gets or sets the billing city.
    /// </summary>
    [StringLength(50)]
    [Display(Name = "City")]
    public string? BillingCity { get; set; }

    /// <summary>
    /// Gets or sets the billing state abbreviation.
    /// </summary>
    [StringLength(2)]
    [Display(Name = "State")]
    public string? BillingState { get; set; }

    /// <summary>
    /// Gets or sets the billing ZIP code.
    /// </summary>
    [StringLength(10)]
    [Display(Name = "ZIP")]
    public string? BillingZIP { get; set; }

    /// <summary>
    /// Gets or sets the customer phone number.
    /// </summary>
    [StringLength(20)]
    [Display(Name = "Phone")]
    public string? CustomerPhone { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
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
