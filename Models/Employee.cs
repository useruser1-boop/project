// File: Employee.cs | Author: Team 05 | Course: ISTM 415
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
    [Display(Name = "First Name")]
    public string EmployeeFirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string EmployeeLastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Social Security Number.
    /// </summary>
    [Required]
    [StringLength(11)]
    [Display(Name = "SSN")]
    public string SSN { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the job title.
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name = "Job Title")]
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hire date.
    /// </summary>
    [Required]
    [Display(Name = "Hire Date")]
    public DateTime HireDate { get; set; }

    /// <summary>
    /// Gets or sets the hourly pay rate.
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Hourly Rate")]
    public decimal HourlyRate { get; set; }

    /// <summary>
    /// Gets the full name.
    /// </summary>
    [NotMapped]
    public string FullName => $"{EmployeeFirstName} {EmployeeLastName}";
}
