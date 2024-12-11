using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moot.Data;
using Moot.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Moot.Controllers
{
    [Authorize(Roles = "OnlySales")]
    public class PropertiesController : Controller
    {
        private readonly LibraryContext _context;

        public PropertiesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Properties
        [AllowAnonymous]
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PropertyTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "PropertyType_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var properties = from b in _context.Property
                        .Include(b => b.Neighborhood)
                        join a in _context.Owner on b.OwnerID equals a.ID
                        join n in _context.Neighborhood on b.NeighborhoodID equals n.ID
                        select new PropertyViewModel
                        {
                            ID = b.ID,
                            PropertyType = b.PropertyType,
                            Price = b.Price,
                            FullName = a.FullName,
                            NeighborhoodName = n.Name
                        };
            if (!String.IsNullOrEmpty(searchString))
            {
                properties = properties.Where(s => s.PropertyType.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "PropertyType_desc":
                    properties = properties.OrderByDescending(b => b.PropertyType);
                    break;
                case "Price":
                    properties = properties.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    properties = properties.OrderByDescending(b => b.Price);
                    break;
                default:
                    properties = properties.OrderBy(b => b.PropertyType);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<PropertyViewModel>.CreateAsync(properties.AsNoTracking(),
            pageNumber ?? 1, pageSize));
        }

        // GET: Properties/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Property
                .Include(b => b.Neighborhood)
                .Include(b => b.Owner)
                .Include(navigationPropertyPath: s => s.Offers)
                .ThenInclude(e => e.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (property == null)
            {
                return NotFound();
            }

            ViewData["OwnerID"] = new SelectList(_context.Set<Owner>(), "ID", "FullName");
            ViewData["NeighborhoodID"] = new SelectList(_context.Set<Neighborhood>(), "ID", "Name");
            return View(property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewData["NeighborhoodID"] = new SelectList(_context.Set<Neighborhood>(), "ID", "Name");
            ViewData["OwnerID"] = new SelectList(_context.Set<Owner>(), "ID", "FullName");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyType,Owner,Price")] Property @property)
        {
            try
            {
            if (ModelState.IsValid)
            {
                _context.Add(property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }

            // ViewBag.Owners = new SelectList(_context.Owner, "ID", "FirstName");

            // ViewData["NeighborhoodID"] = new SelectList(_context.Set<Neighborhood>(), "ID", "ID", property.NeighborhoodID);
            return View(property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Property.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            ViewData["OwnerID"] = new SelectList(_context.Set<Owner>(), "ID", "FullName");
            ViewData["NeighborhoodID"] = new SelectList(_context.Set<Neighborhood>(), "ID", "Name");
            return View(property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookToUpdate = await _context.Property.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Property>(bookToUpdate, "", s => s.OwnerID, s => s.PropertyType, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "FullName",
           bookToUpdate.OwnerID);
            return View(bookToUpdate);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Property
                .Include(b => b.Neighborhood)
                .Include(b => b.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (@property == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }
            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Property.Remove(property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool PropertyExists(int id)
        {
            return _context.Property.Any(e => e.ID == id);
        }

    }
}
