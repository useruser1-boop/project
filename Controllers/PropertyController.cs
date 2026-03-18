// File: PropertyController.cs | Author: Team ## | Course: ISTM 415
using JasperGreen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers;

/// <summary>
/// Manages property CRUD operations.
/// </summary>
public class PropertyController(JasperGreenContext context) : Controller
{
    private readonly JasperGreenContext _context = context;

    /// <summary>
    /// Displays all properties.
    /// </summary>
    /// <returns>The list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var properties = _context.Properties
            .Include(p => p.Customer)
            .Include(p => p.ProvideServices)
            .OrderBy(p => p.Address)
            .ToList();

        return View(properties);
    }

    /// <summary>
    /// Displays add property form.
    /// </summary>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Add()
    {
        LoadCustomers();
        return View("AddEdit", new Property());
    }

    /// <summary>
    /// Creates a property.
    /// </summary>
    /// <param name="property">Property payload.</param>
    /// <returns>Redirect or form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Property property)
    {
        if (!ModelState.IsValid)
        {
            LoadCustomers();
            return View("AddEdit", property);
        }

        _context.Properties.Add(property);
        _context.SaveChanges();
        TempData["message"] = $"Property at {property.Address} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays edit property form.
    /// </summary>
    /// <param name="id">Property identifier.</param>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var property = _context.Properties
            .Include(p => p.Customer)
            .FirstOrDefault(p => p.PropertyID == id);

        if (property is null)
        {
            return RedirectToAction(nameof(List));
        }

        LoadCustomers();
        return View("AddEdit", property);
    }

    /// <summary>
    /// Updates an existing property.
    /// </summary>
    /// <param name="property">Updated property payload.</param>
    /// <returns>Redirect or form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Property property)
    {
        if (!ModelState.IsValid)
        {
            LoadCustomers();
            return View("AddEdit", property);
        }

        _context.Properties.Update(property);
        _context.SaveChanges();
        TempData["message"] = $"Property at {property.Address} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays delete confirmation.
    /// </summary>
    /// <param name="id">Property identifier.</param>
    /// <returns>Delete view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var property = _context.Properties
            .Include(p => p.Customer)
            .FirstOrDefault(p => p.PropertyID == id);

        if (property is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(property);
    }

    /// <summary>
    /// Deletes an existing property.
    /// </summary>
    /// <param name="property">Property payload.</param>
    /// <returns>Redirects to list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Property property)
    {
        var current = _context.Properties.FirstOrDefault(p => p.PropertyID == property.PropertyID);
        if (current is not null)
        {
            _context.Properties.Remove(current);
            _context.SaveChanges();
            TempData["message"] = $"Property at {current.Address} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels current operation and returns to list.
    /// </summary>
    /// <returns>Redirect to list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Loads customer dropdown options used by add/edit property views.
    private void LoadCustomers()
    {
        ViewBag.Customers = _context.Customers
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Select(c => new SelectListItem
            {
                Value = c.CustomerID.ToString(),
                Text = c.FullName
            })
            .ToList();
    }
}
