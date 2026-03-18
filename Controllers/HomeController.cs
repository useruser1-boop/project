// File: HomeController.cs | Author: Team ## | Course: ISTM 415
using Microsoft.AspNetCore.Mvc;

namespace JasperGreen.Controllers;

/// <summary>
/// Handles general informational pages.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Displays the home page.
    /// </summary>
    /// <returns>The index view.</returns>
    [HttpGet]
    public IActionResult Index() => View();

    /// <summary>
    /// Displays company background.
    /// </summary>
    /// <returns>The about view.</returns>
    [HttpGet]
    public IActionResult About() => View();

    /// <summary>
    /// Displays company contact information.
    /// </summary>
    /// <returns>The contact view.</returns>
    [HttpGet]
    public IActionResult ContactUs() => View();
}
