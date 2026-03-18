// File: PaymentController.cs | Author: Team ## | Course: ISTM 415
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
    /// Displays all payments.
    /// </summary>
    /// <returns>Payment list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var payments = _context.Payments
            .Include(p => p.ProvideService)
            .OrderByDescending(p => p.PaymentDate)
            .ToList();

        return View(payments);
    }

    /// <summary>
    /// Displays add payment form.
    /// </summary>
    /// <param name="provideServiceID">Optional preselected service ID.</param>
    /// <returns>Add/edit view.</returns>
    [HttpGet]
    public IActionResult Add(int? provideServiceID)
    {
        LoadProvideServices();
        return View("AddEdit", new Payment
        {
            PaymentDate = DateTime.Now,
            ProvideServiceID = provideServiceID ?? 0
        });
    }

    /// <summary>
    /// Creates a payment.
    /// </summary>
    /// <param name="payment">Payment payload.</param>
    /// <returns>Redirect or form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Payment payment)
    {
        if (!ModelState.IsValid)
        {
            LoadProvideServices();
            return View("AddEdit", payment);
        }

        _context.Payments.Add(payment);
        _context.SaveChanges();
        TempData["message"] = $"Payment #{payment.PaymentID} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays edit payment form.
    /// </summary>
    /// <param name="id">Payment identifier.</param>
    /// <returns>Add/edit view.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var payment = _context.Payments.FirstOrDefault(p => p.PaymentID == id);
        if (payment is null)
        {
            return RedirectToAction(nameof(List));
        }

        LoadProvideServices();
        return View("AddEdit", payment);
    }

    /// <summary>
    /// Updates a payment.
    /// </summary>
    /// <param name="payment">Payment payload.</param>
    /// <returns>Redirect or form view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Payment payment)
    {
        if (!ModelState.IsValid)
        {
            LoadProvideServices();
            return View("AddEdit", payment);
        }

        _context.Payments.Update(payment);
        _context.SaveChanges();
        TempData["message"] = $"Payment #{payment.PaymentID} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays delete confirmation.
    /// </summary>
    /// <param name="id">Payment identifier.</param>
    /// <returns>Delete view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var payment = _context.Payments
            .Include(p => p.ProvideService)
            .FirstOrDefault(p => p.PaymentID == id);
        if (payment is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(payment);
    }

    /// <summary>
    /// Deletes a payment.
    /// </summary>
    /// <param name="payment">Payment payload.</param>
    /// <returns>Redirect to list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Payment payment)
    {
        var current = _context.Payments.FirstOrDefault(p => p.PaymentID == payment.PaymentID);
        if (current is not null)
        {
            _context.Payments.Remove(current);
            _context.SaveChanges();
            TempData["message"] = $"Payment #{current.PaymentID} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels current operation and returns to list.
    /// </summary>
    /// <returns>Redirect to list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Loads ProvideService choices for payment add/edit forms.
    private void LoadProvideServices()
    {
        ViewBag.ProvideServices = _context.ProvideServices
            .OrderByDescending(ps => ps.ServiceDate)
            .Select(ps => new SelectListItem
            {
                Value = ps.ProvideServiceID.ToString(),
                Text = $"#{ps.ProvideServiceID} - {ps.ServiceDate:d}"
            })
            .ToList();
    }
}
