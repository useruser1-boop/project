// File: ProvideServiceController.cs | Author: Team 05 | Course: ISTM 415
// Description: CRUD controller for ProvideService entity with session-based list filtering.
// On my honor, as an Aggie, I have neither given nor received unauthorized aid on this academic work.
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
    private const string FilterCrewKey     = "filterCrewID";

    /// <summary>
    /// Displays all service events, applying any active session filter.
    /// </summary>
    /// <returns>The service event list view.</returns>
    [HttpGet]
    public IActionResult List()
    {
        var qryServices = _context.ProvideServices
            .Include(ps => ps.Customer)
            .Include(ps => ps.Property)
            .Include(ps => ps.Crew)
            .AsQueryable();

        var intCustomerId = HttpContext.Session.GetInt32(FilterCustomerKey);
        var intPropertyId = HttpContext.Session.GetInt32(FilterPropertyKey);
        var intCrewId     = HttpContext.Session.GetInt32(FilterCrewKey);

        if (intCustomerId.HasValue)
        {
            qryServices = qryServices.Where(ps => ps.CustomerID == intCustomerId.Value);
            ViewBag.ActiveFilter = $"Customer ID: {intCustomerId.Value}";
        }
        else if (intPropertyId.HasValue)
        {
            qryServices = qryServices.Where(ps => ps.PropertyID == intPropertyId.Value);
            ViewBag.ActiveFilter = $"Property ID: {intPropertyId.Value}";
        }
        else if (intCrewId.HasValue)
        {
            qryServices = qryServices.Where(ps => ps.CrewID == intCrewId.Value);
            ViewBag.ActiveFilter = $"Crew ID: {intCrewId.Value}";
        }

        return View(qryServices.OrderByDescending(ps => ps.ServiceDate).ToList());
    }

    /// <summary>
    /// Displays the add service event form with today's date pre-filled.
    /// </summary>
    /// <returns>The add/edit view.</returns>
    [HttpGet]
    public IActionResult Add()
    {
        LoadDropdowns();
        return View("AddEdit", new ProvideService { ServiceDate = DateTime.Today });
    }

    /// <summary>
    /// Creates a new service event record.
    /// </summary>
    /// <param name="objProvideService">Service event data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(ProvideService objProvideService)
    {
        ValidateServiceFee(objProvideService);
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return View("AddEdit", objProvideService);
        }

        _context.ProvideServices.Add(objProvideService);
        _context.SaveChanges();
        TempData["message"] = $"Service event #{objProvideService.ServiceID} was added successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the edit form for an existing service event.
    /// </summary>
    /// <param name="id">The service event identifier.</param>
    /// <returns>The add/edit view pre-populated with existing data.</returns>
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var objProvideService = _context.ProvideServices.FirstOrDefault(ps => ps.ServiceID == id);
        if (objProvideService is null) return RedirectToAction(nameof(List));

        LoadDropdowns();
        return View("AddEdit", objProvideService);
    }

    /// <summary>
    /// Updates an existing service event record.
    /// </summary>
    /// <param name="objProvideService">Updated service event data submitted from the form.</param>
    /// <returns>Redirect to list when valid; otherwise re-displays form with errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProvideService objProvideService)
    {
        ValidateServiceFee(objProvideService);
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return View("AddEdit", objProvideService);
        }

        _context.ProvideServices.Update(objProvideService);
        _context.SaveChanges();
        TempData["message"] = $"Service event #{objProvideService.ServiceID} was updated successfully.";
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the delete confirmation page for a service event.
    /// </summary>
    /// <param name="id">The service event identifier.</param>
    /// <returns>The delete confirmation view.</returns>
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var objPs = _context.ProvideServices
            .Include(x => x.Customer)
            .Include(x => x.Property)
            .Include(x => x.Crew)
            .FirstOrDefault(x => x.ServiceID == id);

        if (objPs is null) return RedirectToAction(nameof(List));
        return View(objPs);
    }

    /// <summary>
    /// Permanently removes a service event record from the database.
    /// </summary>
    /// <param name="objProvideService">Service event payload containing the identifier.</param>
    /// <returns>Redirects to the service event list.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(ProvideService objProvideService)
    {
        var objCurrent = _context.ProvideServices
            .FirstOrDefault(ps => ps.ServiceID == objProvideService.ServiceID);
        if (objCurrent is not null)
        {
            _context.ProvideServices.Remove(objCurrent);
            _context.SaveChanges();
            TempData["message"] = $"Service event #{objCurrent.ServiceID} was deleted.";
        }

        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the filter-by-customer selection form.
    /// </summary>
    /// <returns>The get customer view with a customer dropdown.</returns>
    [HttpGet]
    public IActionResult GetCustomer()
    {
        ViewBag.Customers = BuildCustomerOptions();
        return View(new Customer());
    }

    /// <summary>
    /// Stores the selected customer filter in session and returns to the list.
    /// </summary>
    /// <param name="objCustomer">Customer selection from the filter form.</param>
    /// <returns>Redirect to the service event list with the customer filter applied.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetCustomer(Customer objCustomer)
    {
        if (objCustomer.CustomerID <= 0)
        {
            ModelState.AddModelError("", "Please select a customer.");
            ViewBag.Customers = BuildCustomerOptions();
            return View(objCustomer);
        }

        HttpContext.Session.SetInt32(FilterCustomerKey, objCustomer.CustomerID);
        HttpContext.Session.Remove(FilterPropertyKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the filter-by-property selection form.
    /// </summary>
    /// <returns>The get property view with a property dropdown.</returns>
    [HttpGet]
    public IActionResult GetProperty()
    {
        ViewBag.Properties = BuildPropertyOptions();
        return View(new Property());
    }

    /// <summary>
    /// Stores the selected property filter in session and returns to the list.
    /// </summary>
    /// <param name="objProperty">Property selection from the filter form.</param>
    /// <returns>Redirect to the service event list with the property filter applied.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetProperty(Property objProperty)
    {
        HttpContext.Session.SetInt32(FilterPropertyKey, objProperty.PropertyID);
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterCrewKey);
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Displays the filter-by-crew selection form.
    /// </summary>
    /// <returns>The get crew view with a crew dropdown.</returns>
    [HttpGet]
    public IActionResult GetCrew()
    {
        ViewBag.Crews = BuildCrewOptions();
        return View(new Crew());
    }

    /// <summary>
    /// Stores the selected crew filter in session and returns to the list.
    /// </summary>
    /// <param name="objCrew">Crew selection from the filter form.</param>
    /// <returns>Redirect to the service event list with the crew filter applied.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetCrew(Crew objCrew)
    {
        HttpContext.Session.SetInt32(FilterCrewKey, objCrew.CrewID);
        HttpContext.Session.Remove(FilterCustomerKey);
        HttpContext.Session.Remove(FilterPropertyKey);
        return RedirectToAction(nameof(List));
    }

    /// <summary>
    /// Clears all active session filters and returns to the full service event list.
    /// </summary>
    /// <returns>Redirect to the service event list with no filter applied.</returns>
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
    /// Cancels the current operation and returns to the service event list.
    /// </summary>
    /// <returns>Redirect to the list action.</returns>
    [HttpPost]
    public IActionResult Cancel() => RedirectToAction(nameof(List));

    // Ensures the charged fee is not below the property's contracted service fee.
    private void ValidateServiceFee(ProvideService objPs)
    {
        var objProperty = _context.Properties.FirstOrDefault(p => p.PropertyID == objPs.PropertyID);
        if (objProperty is not null && objPs.ServiceFee < objProperty.ServiceFee)
        {
            ModelState.AddModelError(nameof(ProvideService.ServiceFee),
                $"Service fee must be at least {objProperty.ServiceFee:C} for the selected property.");
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
