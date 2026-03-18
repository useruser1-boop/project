// File: EmployeeController.cs | Author: Team ## | Course: ISTM 415
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
    /// Displays all employees.
    /// </summary>
    /// <returns>The list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var employees = _context.Employees
            .AsNoTracking()
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToList();

        return View(employees);
    }

    /// <summary>
    /// Displays add employee form.
    /// </summary>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Add() => View("AddEdit", new Employee { HireDate = DateTime.Today });

    /// <summary>
    /// Creates an employee.
    /// </summary>
    /// <param name="employee">Employee payload.</param>
    /// <returns>Redirect or form view.</returns>
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
    /// Displays edit employee form.
    /// </summary>
    /// <param name="id">Employee identifier.</param>
    /// <returns>Add/edit view.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.EmployeeID == id);
        if (employee is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View("AddEdit", employee);
    }

    /// <summary>
    /// Updates an employee.
    /// </summary>
    /// <param name="employee">Employee payload.</param>
    /// <returns>Redirect or form view.</returns>
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
    /// Displays delete confirmation.
    /// </summary>
    /// <param name="id">Employee identifier.</param>
    /// <returns>Delete view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.EmployeeID == id);
        if (employee is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(employee);
    }

    /// <summary>
    /// Deletes an employee.
    /// </summary>
    /// <param name="employee">Employee payload.</param>
    /// <returns>Redirect to list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Employee employee)
    {
        var current = _context.Employees.FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
        if (current is not null)
        {
            _context.Employees.Remove(current);
            _context.SaveChanges();
            TempData["message"] = $"Employee {current.FullName} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels current operation and returns to list.
    /// </summary>
    /// <returns>Redirect to list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));
}
