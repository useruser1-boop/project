// File: PaymentController.cs | Author: Team 05 | Course: ISTM 415
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

    [HttpGet]
    public IActionResult List()
    {
        var payments = _context.Payments
            .Include(p => p.Customer)
            .OrderByDescending(p => p.PaymentDate)
            .ToList();

        return View(payments);
    }

    [HttpGet]
    public IActionResult Add()
    {
        LoadCustomers();
        return View("AddEdit", new Payment { PaymentDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Payment payment)
    {
        if (!ModelState.IsValid)
        {
            LoadCustomers();
            return View("AddEdit", payment);
        }

        _context.Payments.Add(payment);
        _context.SaveChanges();
        TempData["message"] = $"Payment #{payment.PaymentID} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var payment = _context.Payments.FirstOrDefault(p => p.PaymentID == id);
        if (payment is null) return RedirectToAction(nameof(List));

        LoadCustomers();
        return View("AddEdit", payment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Payment payment)
    {
        if (!ModelState.IsValid)
        {
            LoadCustomers();
            return View("AddEdit", payment);
        }

        _context.Payments.Update(payment);
        _context.SaveChanges();
        TempData["message"] = $"Payment #{payment.PaymentID} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var payment = _context.Payments
            .Include(p => p.Customer)
            .FirstOrDefault(p => p.PaymentID == id);

        if (payment is null) return RedirectToAction(nameof(List));
        return View(payment);
    }

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

    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

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
