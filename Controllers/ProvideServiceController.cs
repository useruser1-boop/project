// File: ProvideServiceController.cs | Author: Team 05 | Course: ISTM 415
using JasperGreen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers;

/// <summary>
/// Manages service event CRUD operations and list filtering.
/// </summary>
public class ProvideServiceController(JasperGreenContext context) : Controller
{
    private readonly JasperGreenContext _context = context;
    private const string FilterCustomerKey  = "filterCustomerID";
    private const string FilterPropertyKey  = "filterPropertyID";
    private const string FilterCrewKey      = "filterCrewID";

    [HttpGet]
    public IActionResult List()
    {
        var query = _context.ProvideServices
            .Include(ps => ps.Customer)
            .Include(ps => ps.Property)
            .Include(ps => ps.Crew)
            .AsQueryable();

        var customerId = HttpContext.Session.GetInt32(FilterCustomerKey);
        var propertyId = HttpContext.Session.GetInt32(FilterPropertyKey);
        var crewId     = HttpContext.Session.GetInt32(FilterCrewKey);

        if (customerId.HasValue)
        {
            query = query.Where(ps => ps.CustomerID == customerId.Value);
            ViewBag.ActiveFilter = $"Customer ID: {customerId.Value}";
        }
        else if (propertyId.HasValue)
        {
            query = query.Where(ps => ps.PropertyID == propertyId.Value);
            ViewBag.ActiveFilter = $"Property ID: {propertyId.Value}";
        }
        else if (crewId.HasValue)
        {
            query = query.Where(ps => ps.CrewID == crewId.Value);
            ViewBag.ActiveFilter = $"Crew ID: {crewId.Value}";
        }

        return View(query.OrderByDescending(ps => ps.ServiceDate).ToList());
    }

    [HttpGet]
    public IActionResult Add()
    {
        LoadDropdowns();
        return View("AddEdit", new ProvideService { ServiceDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(ProvideService provideService)
    {
        ValidateServiceFee(provideService);
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return View("AddEdit", provideService);
        }

        _context.ProvideServices.Add(provideService);
        _context.SaveChanges();
        TempData["message"] = $"Service event #{provideService.ServiceID} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var provideService = _context.ProvideServices.FirstOrDefault(ps => ps.ServiceID == id);
        if (provideService is null) return RedirectToAction(nameof(List));

        LoadDropdowns();
        return View("AddEdit", provideService);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProvideService provideService)
    {
        ValidateServiceFee(provideService);
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return View("AddEdit", provideService);
        }

        _context.ProvideServices.Update(provideService);
        _context.SaveChanges();
        TempData["message"] = $"Service event #{provideService.ServiceID} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var ps = _context.ProvideServices
            .Include(x => x.Customer)
            .Include(x => x.Property)
            .Include(x => x.Crew)
            .FirstOrDefault(x => x.ServiceID == id);

        if (ps is null) return RedirectToAction(nameof(List));
        return View(ps);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(ProvideService provideService)
    {
        var current = _context.ProvideServices.FirstOrDefault(ps => ps.ServiceID == provideService.ServiceID);
        if (current is not null)
        {
            _context.ProvideServices.Remove(current);
            _context.SaveChanges();
            TempData["message"] = $"Service event #{current.ServiceID} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult GetCustomer()
    {
        ViewBag.Customers = BuildCustomerOptions();
        return View(new Customer());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetCustomer(Customer customer)
    {
        if (customer.CustomerID <= 0)
        {
            ModelState.AddModelError("", "Please select a customer.");
            ViewBag.Customers = BuildCustomerOptions();
            return View(customer);
        }

        HttpContext.Session.SetInt32(FilterCustomerKey, customer.CustomerID);
        HttpContext.Session.Remove(FilterPropertyKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult GetProperty()
    {
        ViewBag.Properties = BuildPropertyOptions();
        return View(new Property());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetProperty(Property property)
    {
        HttpContext.Session.SetInt32(FilterPropertyKey, property.PropertyID);
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public IActionResult GetCrew()
    {
        ViewBag.Crews = BuildCrewOptions();
        return View(new Crew());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetCrew(Crew crew)
    {
        HttpContext.Session.SetInt32(FilterCrewKey, crew.CrewID);
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterPropertyKey);
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ClearFilter()
    {
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterPropertyKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Ensures the charged fee is not below the property's contracted service fee.
    private void ValidateServiceFee(ProvideService ps)
    {
        var property = _context.Properties.FirstOrDefault(p => p.PropertyID == ps.PropertyID);
        if (property is not null && ps.ServiceFee < property.ServiceFee)
        {
            ModelState.AddModelError(nameof(ProvideService.ServiceFee),
                $"Service fee must be at least {property.ServiceFee:C} for the selected property.");
        }
    }

    private void LoadDropdowns()
    {
        ViewBag.Customers  = BuildCustomerOptions();
        ViewBag.Properties = BuildPropertyOptions();
        ViewBag.Crews      = BuildCrewOptions();
    }

    private List<SelectListItem> BuildCustomerOptions() => _context.Customers
        .OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
        .Select(c => new SelectListItem { Value = c.CustomerID.ToString(), Text = c.FullName })
        .ToList();

    private List<SelectListItem> BuildPropertyOptions() => _context.Properties
        .OrderBy(p => p.PropertyAddress)
        .Select(p => new SelectListItem { Value = p.PropertyID.ToString(), Text = p.PropertyAddress })
        .ToList();

    private List<SelectListItem> BuildCrewOptions() => _context.Crews
        .OrderBy(c => c.CrewName)
        .Select(c => new SelectListItem { Value = c.CrewID.ToString(), Text = c.CrewName })
        .ToList();
}
