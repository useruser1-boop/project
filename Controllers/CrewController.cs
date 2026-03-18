// File: CrewController.cs | Author: Team ## | Course: ISTM 415
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
    /// Displays all crews.
    /// </summary>
    /// <returns>The list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var crews = _context.Crews
            .Include(c => c.CrewForeman)
            .Include(c => c.CrewMember1)
            .Include(c => c.CrewMember2)
            .OrderBy(c => c.CrewName)
            .Select(c => new CrewViewModel { Crew = c })
            .ToList();

        return View(crews);
    }

    /// <summary>
    /// Displays add crew form.
    /// </summary>
    /// <returns>Add/edit view with dropdown values.</returns>
    [HttpGet]
    public IActionResult Add() => View("AddEdit", BuildViewModel(new Crew()));

    /// <summary>
    /// Creates a crew.
    /// </summary>
    /// <param name="viewModel">Crew view model payload.</param>
    /// <returns>Redirect or form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(CrewViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("AddEdit", BuildViewModel(viewModel.Crew));
        }

        // Ensure foreman/member assignments are unique per project rules.
        if (!AreDistinctEmployees(viewModel.Crew))
        {
            ModelState.AddModelError("", "Crew Foreman and both Crew Members must be three distinct employees.");
            return View("AddEdit", BuildViewModel(viewModel.Crew));
        }

        _context.Crews.Add(viewModel.Crew);
        _context.SaveChanges();
        TempData["message"] = $"Crew {viewModel.Crew.CrewName} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays edit crew form.
    /// </summary>
    /// <param name="id">Crew identifier.</param>
    /// <returns>Add/edit view.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var crew = _context.Crews.FirstOrDefault(c => c.CrewID == id);
        if (crew is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View("AddEdit", BuildViewModel(crew));
    }

    /// <summary>
    /// Updates a crew.
    /// </summary>
    /// <param name="viewModel">Crew view model payload.</param>
    /// <returns>Redirect or form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CrewViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("AddEdit", BuildViewModel(viewModel.Crew));
        }

        // Ensure foreman/member assignments are unique per project rules.
        if (!AreDistinctEmployees(viewModel.Crew))
        {
            ModelState.AddModelError("", "Crew Foreman and both Crew Members must be three distinct employees.");
            return View("AddEdit", BuildViewModel(viewModel.Crew));
        }

        _context.Crews.Update(viewModel.Crew);
        _context.SaveChanges();
        TempData["message"] = $"Crew {viewModel.Crew.CrewName} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays delete confirmation.
    /// </summary>
    /// <param name="id">Crew identifier.</param>
    /// <returns>Delete view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var crew = _context.Crews
            .Include(c => c.CrewForeman)
            .Include(c => c.CrewMember1)
            .Include(c => c.CrewMember2)
            .FirstOrDefault(c => c.CrewID == id);

        if (crew is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(crew);
    }

    /// <summary>
    /// Deletes a crew.
    /// </summary>
    /// <param name="crew">Crew payload.</param>
    /// <returns>Redirect to list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Crew crew)
    {
        var current = _context.Crews.FirstOrDefault(c => c.CrewID == crew.CrewID);
        if (current is not null)
        {
            _context.Crews.Remove(current);
            _context.SaveChanges();
            TempData["message"] = $"Crew {current.CrewName} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels current operation and returns to list.
    /// </summary>
    /// <returns>Redirect to list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Builds the crew view model and employee dropdown list for all three selectors.
    private CrewViewModel BuildViewModel(Crew crew)
    {
        var employeeOptions = _context.Employees
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .Select(e => new SelectListItem
            {
                Value = e.EmployeeID.ToString(),
                Text = e.FullName
            })
            .ToList();

        return new CrewViewModel
        {
            Crew = crew,
            Employees = employeeOptions
        };
    }

    // Custom validation rule that requires foreman/member IDs to be distinct.
    private static bool AreDistinctEmployees(Crew crew)
    {
        return crew.CrewForemanID != crew.CrewMember1ID
            && crew.CrewForemanID != crew.CrewMember2ID
            && crew.CrewMember1ID != crew.CrewMember2ID;
    }
}
