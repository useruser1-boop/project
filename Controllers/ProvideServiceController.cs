// File: ProvideServiceController.cs | Author: Team ## | Course: ISTM 415
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
    private const string FilterCustomerKey = "filterCustomerID";
    private const string FilterPropertyKey = "filterPropertyID";
    private const string FilterCrewKey = "filterCrewID";

    /// <summary>
    /// Displays service events with the active session filter.
    /// </summary>
    /// <returns>The filtered list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var query = _context.ProvideServices
            .Include(ps => ps.Customer)
            .Include(ps => ps.Property)
            .Include(ps => ps.Crew)
            .AsQueryable();

        // Read session keys in priority order and apply the first active filter.
        var customerId = HttpContext.Session.GetInt32(FilterCustomerKey);
        var propertyId = HttpContext.Session.GetInt32(FilterPropertyKey);
        var crewId = HttpContext.Session.GetInt32(FilterCrewKey);

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

        var services = query.OrderByDescending(ps => ps.ServiceDate).ToList();
        return View(services);
    }

    /// <summary>
    /// Displays add service form.
    /// </summary>
    /// <returns>Add/edit view with defaults.</returns>
    [HttpGet]
    public IActionResult Add()
    {
        LoadDropdowns();
        return View("AddEdit", new ProvideService { ServiceDate = DateTime.Now });
    }

    /// <summary>
    /// Creates a new service event.
    /// </summary>
    /// <param name="provideService">Service event payload.</param>
    /// <returns>Redirect or form view.</returns>
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
        TempData["message"] = $"Service event #{provideService.ProvideServiceID} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays edit service form.
    /// </summary>
    /// <param name="id">Service event identifier.</param>
    /// <returns>Add/edit view.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var provideService = _context.ProvideServices.FirstOrDefault(ps => ps.ProvideServiceID == id);
        if (provideService is null)
        {
            return RedirectToAction(nameof(List));
        }

        LoadDropdowns();
        return View("AddEdit", provideService);
    }

    /// <summary>
    /// Updates an existing service event.
    /// </summary>
    /// <param name="provideService">Service event payload.</param>
    /// <returns>Redirect or form view.</returns>
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
        TempData["message"] = $"Service event #{provideService.ProvideServiceID} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays delete confirmation.
    /// </summary>
    /// <param name="id">Service event identifier.</param>
    /// <returns>Delete view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var provideService = _context.ProvideServices
            .Include(ps => ps.Customer)
            .Include(ps => ps.Property)
            .Include(ps => ps.Crew)
            .FirstOrDefault(ps => ps.ProvideServiceID == id);

        if (provideService is null)
        {
            return RedirectToAction(nameof(List));
        }

        return View(provideService);
    }

    /// <summary>
    /// Deletes a service event.
    /// </summary>
    /// <param name="provideService">Service event payload.</param>
    /// <returns>Redirect to list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(ProvideService provideService)
    {
        var current = _context.ProvideServices.FirstOrDefault(ps => ps.ProvideServiceID == provideService.ProvideServiceID);
        if (current is not null)
        {
            _context.ProvideServices.Remove(current);
            _context.SaveChanges();
            TempData["message"] = $"Service event #{current.ProvideServiceID} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Shows customer filter page.
    /// </summary>
    /// <returns>GetCustomer view.</returns>
    [HttpGet]
    public IActionResult GetCustomer()
    {
        ViewBag.Customers = BuildCustomerOptions();
        return View(new Customer());
    }

    /// <summary>
    /// Applies customer filter in session.
    /// </summary>
    /// <param name="customer">Customer selection payload.</param>
    /// <returns>Redirect to filtered list or return selection view.</returns>
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

        // Store selected customer in session and clear other filter keys.
        HttpContext.Session.SetInt32(FilterCustomerKey, customer.CustomerID);
        HttpContext.Session.Remove(FilterPropertyKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Shows property filter page.
    /// </summary>
    /// <returns>GetProperty view.</returns>
    [HttpGet]
    public IActionResult GetProperty()
    {
        ViewBag.Properties = BuildPropertyOptions();
        return View(new Property());
    }

    /// <summary>
    /// Applies property filter in session.
    /// </summary>
    /// <param name="property">Property selection payload.</param>
    /// <returns>Redirect to filtered list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetProperty(Property property)
    {
        HttpContext.Session.SetInt32(FilterPropertyKey, property.PropertyID);
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Shows crew filter page.
    /// </summary>
    /// <returns>GetCrew view.</returns>
    [HttpGet]
    public IActionResult GetCrew()
    {
        ViewBag.Crews = BuildCrewOptions();
        return View(new Crew());
    }

    /// <summary>
    /// Applies crew filter in session.
    /// </summary>
    /// <param name="crew">Crew selection payload.</param>
    /// <returns>Redirect to filtered list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetCrew(Crew crew)
    {
        HttpContext.Session.SetInt32(FilterCrewKey, crew.CrewID);
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterPropertyKey);
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Clears all list filters in session.
    /// </summary>
    /// <returns>Redirect to list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ClearFilter()
    {
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterPropertyKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Cancels current operation and returns to list.
    /// </summary>
    /// <returns>Redirect to list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Custom business rule: the charged amount cannot be below the linked property's contracted fee.
    private void ValidateServiceFee(ProvideService provideService)
    {
        var property = _context.Properties.FirstOrDefault(p => p.PropertyID == provideService.PropertyID);
        if (property is not null && provideService.ServiceFeeCharged < property.ServiceFee)
        {
            ModelState.AddModelError(nameof(ProvideService.ServiceFeeCharged),
                $"Service fee charged must be at least {property.ServiceFee:C} for the selected property.");
        }
    }

    // Loads dropdown data for add/edit service screens.
    private void LoadDropdowns()
    {
        ViewBag.Customers = BuildCustomerOptions();
        ViewBag.Properties = BuildPropertyOptions();
        ViewBag.Crews = BuildCrewOptions();
    }

    // Builds customer select list items.
    private List<SelectListItem> BuildCustomerOptions() => _context.Customers
        .OrderBy(c => c.LastName)
        .ThenBy(c => c.FirstName)
        .Select(c => new SelectListItem { Value = c.CustomerID.ToString(), Text = c.FullName })
        .ToList();

    // Builds property select list items.
    private List<SelectListItem> BuildPropertyOptions() => _context.Properties
        .OrderBy(p => p.Address)
        .Select(p => new SelectListItem { Value = p.PropertyID.ToString(), Text = p.Address })
        .ToList();

    // Builds crew select list items.
    private List<SelectListItem> BuildCrewOptions() => _context.Crews
        .OrderBy(c => c.CrewName)
        .Select(c => new SelectListItem { Value = c.CrewID.ToString(), Text = c.CrewName })
        .ToList();
}
