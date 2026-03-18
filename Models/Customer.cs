// File: Customer.cs | Author: Team ## | Course: ISTM 415
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
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    [StringLength(100)]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    [StringLength(100)]
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    [StringLength(50)]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the 2-letter state abbreviation.
    /// </summary>
    [StringLength(2)]
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the ZIP code.
    /// </summary>
    [StringLength(10)]
    public string? ZipCode { get; set; }

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
