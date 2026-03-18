// File: Employee.cs | Author: Team ## | Course: ISTM 415
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models;

/// <summary>
/// Represents a Jasper Green employee.
/// </summary>
public class Employee
{
    /// <summary>
    /// Gets or sets the employee identifier.
    /// </summary>
    public int EmployeeID { get; set; }

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
    /// Gets or sets the hire date.
    /// </summary>
    [Required]
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Gets the full name.
    /// </summary>
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}
