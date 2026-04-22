// File: CrewController.cs | Author: Team 05 | Course: ISTM 415
// Description: CRUD controller for Crew entity.
// On my honor, as an Aggie, I have neither given nor received unauthorized aid on this academic work.
using JasperGreen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers;

/// <summary>
/// Manages crew CRUD operations.
/// </summary>
public class CrewController(JasperGreenContext context) : Controller
{
    private readonly JasperGreenContext _context = context;

    /// <summary>
    /// Displays all crews with foreman and member names.
    /// </summary>
    /// <returns>The crew list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var lstCrews = _context.Crews
            .Include(c => c.CrewForeman)
            .Include(c => c.CrewMember1)
            .Include(c => c.CrewMember2)
            .OrderBy(c => c.CrewName)
            .ToList();

        return View(lstCrews);
    }

    /// <summary>
    /// Displays the add crew form with employee dropdowns for foreman and members.
    /// </summary>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Add()
    {
        LoadEmployees();
        return View("AddEdit", new Crew());
    }

    /// <summary>
    /// Creates a new crew record.
    /// </summary>
    /// <param name="crew">Crew data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Crew crew)
    {
        if (!ModelState.IsValid)
        {
            LoadEmployees();
            return View("AddEdit", crew);
        }

        // Foreman and both crew members must be three distinct employees.
        if (!AreDistinctEmployees(crew))
        {
            ModelState.AddModelError("", "Crew Foreman and both Crew Members must be three distinct employees.");
            LoadEmployees();
            return View("AddEdit", crew);
        }

        _context.Crews.Add(crew);
        _context.SaveChanges();
        TempData["message"] = $"Crew {crew.CrewName} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the edit form for an existing crew with employee dropdowns pre-selected.
    /// </summary>
    /// <param name="id">The crew identifier.</param>
    /// <returns>The add/edit view pre-populated with existing crew data.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var objCrew = _context.Crews.FirstOrDefault(c => c.CrewID == id);
        if (objCrew is null)
        {
            return RedirectToAction(nameof(List));
        }

        LoadEmployees();
        return View("AddEdit", objCrew);
    }

    /// <summary>
    /// Updates an existing crew record.
    /// </summary>
    /// <param name="crew">Updated crew data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Crew crew)
    {
        if (!ModelState.IsValid)
        {
            LoadEmployees();
            return View("AddEdit", crew);
        }

        // Foreman and both crew members must be three distinct employees.
        if (!AreDistinctEmployees(crew))
        {
            ModelState.AddModelError("", "Crew Foreman and both Crew Members must be three distinct employees.");
            LoadEmployees();
            return View("AddEdit", crew);
        }

        _context.Crews.Update(crew);
        _context.SaveChanges();
        TempData["message"] = $"Crew {crew.CrewName} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the delete confirmation page for a crew.
    /// </summary>
    /// <param name="id">The crew identifier.</param>
    /// <returns>The delete confirmation view with foreman and member details.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var objCrew = _context.Crews
            .Include(c => c.CrewForeman)
            .Include(c => c.CrewMember1)
            .Include(c => c.CrewMember2)
            .FirstOrDefault(c => c.CrewID == id);

        if (objCrew is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(objCrew);
    }

    /// <summary>
    /// Permanently removes a crew record from the database.
    /// Redirects back to the confirmation page with an error if service records prevent deletion.
    /// </summary>
    /// <param name="crew">Crew payload containing the identifier.</param>
    /// <returns>Redirects to the crew list on success; returns delete view on constraint violation.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Crew crew)
    {
        var objCurrent = _context.Crews.FirstOrDefault(c => c.CrewID == crew.CrewID);
        if (objCurrent is not null)
        {
            try
            {
                _context.Crews.Remove(objCurrent);
                _context.SaveChanges();
                TempData["message"] = $"Crew {objCurrent.CrewName} was deleted.";
            }
            catch (DbUpdateException)
            {
                TempData["error"] = $"Cannot delete crew {objCurrent.CrewName} because it has associated service records.";
                return RedirectToAction(nameof(Delete), new { id = objCurrent.CrewID });
            }
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels the current operation and returns to the crew list.
    /// </summary>
    /// <returns>Redirect to the list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Populates ViewBag.Employees with all employees sorted by name for the three crew dropdowns.
    private void LoadEmployees()
    {
        var lstEmployeeOptions = _context.Employees
            .OrderBy(e => e.EmployeeLastName)
            .ThenBy(e => e.EmployeeFirstName)
            .Select(e => new SelectListItem
            {
                Value = e.EmployeeID.ToString(),
                Text = e.FullName
            })
            .ToList();

        ViewBag.Employees = lstEmployeeOptions;
    }

    // Validates that the foreman and two crew members are three distinct employees.
    private static bool AreDistinctEmployees(Crew crew)
    {
        return crew.CrewForemanID != crew.CrewMember1ID
            && crew.CrewForemanID != crew.CrewMember2ID
            && crew.CrewMember1ID != crew.CrewMember2ID;
    }  
}
