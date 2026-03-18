// File: Crew.cs | Author: Team ## | Course: ISTM 415
using System.ComponentModel.DataAnnotations;

namespace JasperGreen.Models;

/// <summary>
/// Represents a lawn service crew.
/// </summary>
public class Crew
{
    /// <summary>
    /// Gets or sets the crew identifier.
    /// </summary>
    public int CrewID { get; set; }

    /// <summary>
    /// Gets or sets the crew name.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string CrewName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the foreman employee identifier.
    /// </summary>
    [Required]
    public int CrewForemanID { get; set; }

    /// <summary>
    /// Gets or sets the first crew member employee identifier.
    /// </summary>
    [Required]
    public int CrewMember1ID { get; set; }

    /// <summary>
    /// Gets or sets the second crew member employee identifier.
    /// </summary>
    [Required]
    public int CrewMember2ID { get; set; }

    /// <summary>
    /// Gets or sets the foreman.
    /// </summary>
    public Employee? CrewForeman { get; set; }

    /// <summary>
    /// Gets or sets crew member 1.
    /// </summary>
    public Employee? CrewMember1 { get; set; }

    /// <summary>
    /// Gets or sets crew member 2.
    /// </summary>
    public Employee? CrewMember2 { get; set; }

    /// <summary>
    /// Gets or sets service events assigned to the crew.
    /// </summary>
    public ICollection<ProvideService> ProvideServices { get; set; } = new List<ProvideService>();
}
