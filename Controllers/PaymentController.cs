// File: PaymentController.cs | Author: Team 05 | Course: ISTM 415
// Description: CRUD controller for Payment entity, with optional linking to a service event.
// On my honor, as an Aggie, I have neither given nor received unauthorized aid on this academic work.
using JasperGreen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers;

/// <summary>
/// Manages payment CRUD operations.
/// </summary>
public class PaymentController(JasperGreenContext context) : Controller
{
    private readonly JasperGreenContext _context = context;

    /// <summary>
    /// Displays all payments sorted by date descending.
    /// </summary>
    /// <returns>The payment list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var lstPayments = _context.Payments
            .Include(p => p.Customer)
            .OrderByDescending(p => p.PaymentDate)
            .ToList();

        return View(lstPayments);
    }

    /// <summary>
    /// Displays the add payment form.
    /// When a service event identifier is provided, pre-fills the customer from that service.
    /// </summary>
    /// <param name="serviceId">Optional service event identifier to link this payment to.</param>
    /// <returns>The add/edit view with today's date pre-filled.</returns>
    [HttpGet]
    public IActionResult Add(int? serviceId = null)
    {
        var objPayment = new Payment { PaymentDate = DateTime.Today };

        if (serviceId.HasValue)
        {
            var objService = _context.ProvideServices
                .FirstOrDefault(ps => ps.ServiceID == serviceId.Value);
            if (objService is not null)
                objPayment.CustomerID = objService.CustomerID;
        }

        ViewBag.ServiceId = serviceId;
        LoadCustomers();
        return View("AddEdit", objPayment);
    }

    /// <summary>
    /// Creates a new payment record.
    /// If a service event identifier is provided, links the new payment to that service event.
    /// </summary>
    /// <param name="objPayment">Payment data submitted from the form.</param>
    /// <param name="serviceId">Optional service event identifier to link this payment to.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Payment objPayment, int? serviceId = null)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ServiceId = serviceId;
            LoadCustomers();
            return View("AddEdit", objPayment);
        }

        _context.Payments.Add(objPayment);
        _context.SaveChanges();

        if (serviceId.HasValue)
        {
            var objService = _context.ProvideServices
                .FirstOrDefault(ps => ps.ServiceID == serviceId.Value);
            if (objService is not null)
            {
                objService.PaymentID = objPayment.PaymentID;
                _context.ProvideServices.Update(objService);
                _context.SaveChanges();
            }
        }

        TempData["message"] = $"Payment #{objPayment.PaymentID} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the edit form for an existing payment.
    /// </summary>
    /// <param name="id">The payment identifier.</param>
    /// <returns>The add/edit view pre-populated with existing data.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var objPayment = _context.Payments.FirstOrDefault(p => p.PaymentID == id);
        if (objPayment is null) return RedirectToAction(nameof(List));

        LoadCustomers();
        return View("AddEdit", objPayment);
    }

    /// <summary>
    /// Updates an existing payment record.
    /// </summary>
    /// <param name="objPayment">Updated payment data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Payment objPayment)
    {
        if (!ModelState.IsValid)
        {
            LoadCustomers();
            return View("AddEdit", objPayment);
        }

        _context.Payments.Update(objPayment);
        _context.SaveChanges();
        TempData["message"] = $"Payment #{objPayment.PaymentID} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the delete confirmation page for a payment.
    /// </summary>
    /// <param name="id">The payment identifier.</param>
    /// <returns>The delete confirmation view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var objPayment = _context.Payments
            .Include(p => p.Customer)
            .FirstOrDefault(p => p.PaymentID == id);

        if (objPayment is null) return RedirectToAction(nameof(List));
        return View(objPayment);
    }

    /// <summary>
    /// Permanently removes a payment record from the database.
    /// The related service event's PaymentID is automatically set to null by the database.
    /// </summary>
    /// <param name="objPayment">Payment payload containing the identifier.</param>
    /// <returns>Redirects to the payment list on success.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Payment objPayment)
    {
        var objCurrent = _context.Payments.FirstOrDefault(p => p.PaymentID == objPayment.PaymentID);
        if (objCurrent is not null)
        {
            _context.Payments.Remove(objCurrent);
            _context.SaveChanges();
            TempData["message"] = $"Payment #{objCurrent.PaymentID} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels the current operation and returns to the payment list.
    /// </summary>
    /// <returns>Redirect to the list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Populates ViewBag.Customers with all customers sorted by name for the payment form dropdown.
    private void LoadCustomers()
    {
        ViewBag.Customers = _context.Customers
            .OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
            .Select(c => new SelectListItem
            {
                Value = c.CustomerID.ToString(),
                Text  = c.FullName
            })
            .ToList();
    }
}
