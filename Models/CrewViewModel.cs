// File: CrewViewModel.cs | Author: Team ## | Course: ISTM 415
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JasperGreen.Models;

/// <summary>
/// View model for crew add/edit screens.
/// </summary>
public class CrewViewModel
{
    /// <summary>
    /// Gets or sets the crew.
    /// </summary>
    public Crew Crew { get; set; } = new Crew();

    /// <summary>
    /// Gets or sets all employee options.
    /// </summary>
    public IEnumerable<SelectListItem> Employees { get; set; } = Enumerable.Empty<SelectListItem>();
}
