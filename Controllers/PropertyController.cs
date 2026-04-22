// File: PropertyController.cs | Author: Team 05 | Course: ISTM 415
// Description: CRUD controller for Property entity.
// On my honor, as an Aggie, I have neither given nor received unauthorized aid on this academic work.
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
    /// Displays all properties sorted by address, with owning customer information.
    /// </summary>
    /// <returns>The property list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var lstProperties = _context.Properties
            .Include(p => p.Customer)
            .Include(p => p.ProvideServices)
            .OrderBy(p => p.PropertyAddress)
            .ToList();

        return View(lstProperties);
    }

    /// <summary>
    /// Displays the add property form with a customer dropdown.
    /// </summary>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Add()
    {
        LoadCustomers();
        return View("AddEdit", new Property());
    }

    /// <summary>
    /// Creates a new property record.
    /// </summary>
    /// <param name="property">Property data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
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
        TempData["message"] = $"Property at {property.PropertyAddress} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the edit form for an existing property.
    /// </summary>
    /// <param name="id">The property identifier.</param>
    /// <returns>The add/edit view pre-populated with existing data and customer dropdown pre-selected.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var objProperty = _context.Properties
            .Include(p => p.Customer)
            .FirstOrDefault(p => p.PropertyID == id);

        if (objProperty is null)
        {
            return RedirectToAction(nameof(List));
        }

        LoadCustomers();
        return View("AddEdit", objProperty);
    }

    /// <summary>
    /// Updates an existing property record.
    /// </summary>
    /// <param name="property">Updated property data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
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
        TempData["message"] = $"Property at {property.PropertyAddress} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the delete confirmation page for a property.
    /// </summary>
    /// <param name="id">The property identifier.</param>
    /// <returns>The delete confirmation view with property and customer details.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var objProperty = _context.Properties
            .Include(p => p.Customer)
            .FirstOrDefault(p => p.PropertyID == id);

        if (objProperty is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(objProperty);
    }

    /// <summary>
    /// Permanently removes a property record from the database.
    /// Redirects back to the confirmation page with an error if related service records prevent deletion.
    /// </summary>
    /// <param name="property">Property payload containing the identifier.</param>
    /// <returns>Redirects to the property list on success; returns delete view on constraint violation.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Property property)
    {
        var objCurrent = _context.Properties.FirstOrDefault(p => p.PropertyID == property.PropertyID);
        if (objCurrent is not null)
        {
            try
            {
                _context.Properties.Remove(objCurrent);
                _context.SaveChanges();
                TempData["message"] = $"Property at {objCurrent.PropertyAddress} was deleted.";
            }
            catch (DbUpdateException)
            {
                TempData["error"] = $"Cannot delete the property at {objCurrent.PropertyAddress} because it has associated service records.";
                return RedirectToAction(nameof(Delete), new { id = objCurrent.PropertyID });
            }
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels the current operation and returns to the property list.
    /// </summary>
    /// <returns>Redirect to the list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Populates ViewBag.Customers with customer full names for the property ownership dropdown.
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
