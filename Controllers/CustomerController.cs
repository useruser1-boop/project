// File: CustomerController.cs | Author: Team ## | Course: ISTM 415
using JasperGreen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers;

/// <summary>
/// Manages customer CRUD operations.
/// </summary>
public class CustomerController(JasperGreenContext context) : Controller
{
    private readonly JasperGreenContext _context = context;

    /// <summary>
    /// Displays all customers.
    /// </summary>
    /// <returns>The customer list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var customers = _context.Customers
            .Include(c => c.Properties)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToList();

        return View(customers);
    }

    /// <summary>
    /// Displays the add customer form.
    /// </summary>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Add()
    {
        LoadStates();
        return View("AddEdit", new Customer());
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customer">Customer data.</param>
    /// <returns>Redirect to list when valid; otherwise form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            LoadStates();
            return View("AddEdit", customer);
        }

        _context.Customers.Add(customer);
        _context.SaveChanges();
        TempData["message"] = $"Customer {customer.FullName} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the edit customer form.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var customer = _context.Customers
            .Include(c => c.Properties)
            .FirstOrDefault(c => c.CustomerID == id);

        if (customer is null)
        {
            return RedirectToAction(nameof(List));
        }

        LoadStates();
        return View("AddEdit", customer);
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="customer">Updated customer data.</param>
    /// <returns>Redirect to list when valid; otherwise form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            LoadStates();
            return View("AddEdit", customer);
        }

        _context.Customers.Update(customer);
        _context.SaveChanges();
        TempData["message"] = $"Customer {customer.FullName} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays delete confirmation.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <returns>The delete view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.CustomerID == id);
        if (customer is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(customer);
    }

    /// <summary>
    /// Deletes an existing customer.
    /// </summary>
    /// <param name="customer">Customer payload.</param>
    /// <returns>Redirects to list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Customer customer)
    {
        var current = _context.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
        if (current is not null)
        {
            _context.Customers.Remove(current);
            _context.SaveChanges();
            TempData["message"] = $"Customer {current.FullName} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels current operation and returns to list.
    /// </summary>
    /// <returns>Redirect to list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Loads state abbreviations for customer dropdown selection.
    private void LoadStates()
    {
        ViewBag.States = new List<SelectListItem>
        {
            new() { Value = "", Text = "Select a state" },
            new() { Value = "TX", Text = "TX" },
            new() { Value = "AL", Text = "AL" }, new() { Value = "AK", Text = "AK" }, new() { Value = "AZ", Text = "AZ" },
            new() { Value = "AR", Text = "AR" }, new() { Value = "CA", Text = "CA" }, new() { Value = "CO", Text = "CO" },
            new() { Value = "CT", Text = "CT" }, new() { Value = "DE", Text = "DE" }, new() { Value = "FL", Text = "FL" },
            new() { Value = "GA", Text = "GA" }, new() { Value = "HI", Text = "HI" }, new() { Value = "ID", Text = "ID" },
            new() { Value = "IL", Text = "IL" }, new() { Value = "IN", Text = "IN" }, new() { Value = "IA", Text = "IA" },
            new() { Value = "KS", Text = "KS" }, new() { Value = "KY", Text = "KY" }, new() { Value = "LA", Text = "LA" },
            new() { Value = "ME", Text = "ME" }, new() { Value = "MD", Text = "MD" }, new() { Value = "MA", Text = "MA" },
            new() { Value = "MI", Text = "MI" }, new() { Value = "MN", Text = "MN" }, new() { Value = "MS", Text = "MS" },
            new() { Value = "MO", Text = "MO" }, new() { Value = "MT", Text = "MT" }, new() { Value = "NE", Text = "NE" },
            new() { Value = "NV", Text = "NV" }, new() { Value = "NH", Text = "NH" }, new() { Value = "NJ", Text = "NJ" },
            new() { Value = "NM", Text = "NM" }, new() { Value = "NY", Text = "NY" }, new() { Value = "NC", Text = "NC" },
            new() { Value = "ND", Text = "ND" }, new() { Value = "OH", Text = "OH" }, new() { Value = "OK", Text = "OK" },
            new() { Value = "OR", Text = "OR" }, new() { Value = "PA", Text = "PA" }, new() { Value = "RI", Text = "RI" },
            new() { Value = "SC", Text = "SC" }, new() { Value = "SD", Text = "SD" }, new() { Value = "TN", Text = "TN" },
            new() { Value = "UT", Text = "UT" }, new() { Value = "VT", Text = "VT" }, new() { Value = "VA", Text = "VA" },
            new() { Value = "WA", Text = "WA" }, new() { Value = "WV", Text = "WV" }, new() { Value = "WI", Text = "WI" },
            new() { Value = "WY", Text = "WY" }
        };
    }
}
