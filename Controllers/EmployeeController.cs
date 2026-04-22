// File: EmployeeController.cs | Author: Team 05 | Course: ISTM 415
// Description: CRUD controller for Employee entity.
// On my honor, as an Aggie, I have neither given nor received unauthorized aid on this academic work.
using JasperGreen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers;

/// <summary>
/// Manages employee CRUD operations.
/// </summary>
public class EmployeeController(JasperGreenContext context) : Controller
{
    private readonly JasperGreenContext _context = context;

    /// <summary>
    /// Displays all employees sorted by last name, then first name.
    /// </summary>
    /// <returns>The employee list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var lstEmployees = _context.Employees
            .AsNoTracking()
            .OrderBy(e => e.EmployeeLastName)
            .ThenBy(e => e.EmployeeFirstName)
            .ToList();

        return View(lstEmployees);
    }

    /// <summary>
    /// Displays the add employee form with today's date pre-filled for hire date.
    /// </summary>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Add() => View("AddEdit", new Employee { HireDate = DateTime.Today });

    /// <summary>
    /// Creates a new employee record.
    /// </summary>
    /// <param name="employee">Employee data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View("AddEdit", employee);
        }

        _context.Employees.Add(employee);
        _context.SaveChanges();
        TempData["message"] = $"Employee {employee.FullName} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the edit form for an existing employee.
    /// </summary>
    /// <param name="id">The employee identifier.</param>
    /// <returns>The add/edit view pre-populated with existing data.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var objEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeID == id);
        if (objEmployee is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View("AddEdit", objEmployee);
    }

    /// <summary>
    /// Updates an existing employee record.
    /// </summary>
    /// <param name="employee">Updated employee data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View("AddEdit", employee);
        }

        _context.Employees.Update(employee);
        _context.SaveChanges();
        TempData["message"] = $"Employee {employee.FullName} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the delete confirmation page for an employee.
    /// </summary>
    /// <param name="id">The employee identifier.</param>
    /// <returns>The delete confirmation view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var objEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeID == id);
        if (objEmployee is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(objEmployee);
    }

    /// <summary>
    /// Permanently removes an employee record from the database.
    /// Redirects back to the confirmation page with an error if crew assignments prevent deletion.
    /// </summary>
    /// <param name="employee">Employee payload containing the identifier.</param>
    /// <returns>Redirects to the employee list on success; returns delete view on constraint violation.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Employee employee)
    {
        var objCurrent = _context.Employees.FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
        if (objCurrent is not null)
        {
            try
            {
                _context.Employees.Remove(objCurrent);
                _context.SaveChanges();
                TempData["message"] = $"Employee {objCurrent.FullName} was deleted.";
            }
            catch (DbUpdateException)
            {
                TempData["error"] = $"Cannot delete {objCurrent.FullName} because they are assigned to one or more crews.";
                return RedirectToAction(nameof(Delete), new { id = objCurrent.EmployeeID });
            }
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels the current operation and returns to the employee list.
    /// </summary>
    /// <returns>Redirect to the list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));
}
