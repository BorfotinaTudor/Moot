using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moot.Models.LibraryViewModels;
using Moot.Data;
using Moot.Models;
using Microsoft.AspNetCore.Authorization;

namespace Moot.Controllers
{
    [Authorize(Roles = "OnlySales")]
    public class AgentsController : Controller
    {
        private readonly LibraryContext _context;

        public AgentsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Agents
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id, int? propertyID)
        {
            var viewModel = new AgentIndexData();

            viewModel.Agents = await _context.Agent
            .Include(p => p.PublishedProperties)
                    .ThenInclude(pb => pb.Property)
                .ThenInclude(b => b.Owner) // Include autorii cărților
                .Include(p => p.PublishedProperties)
                    .ThenInclude(pb => pb.Property)
                .ThenInclude(b => b.Offers)
                    .ThenInclude(o => o.Client) // Include clienții pentru comenzi
                .AsNoTracking()
                .OrderBy(p => p.AgentName)
                .ToListAsync();

            if (id != null)
            {
                ViewData["AgentID"] = id.Value;
                Agent agent = viewModel.Agents.Where(
                i => i.ID == id.Value).Single();
                viewModel.Properties = agent.PublishedProperties.Select(s => s.Property);
            }
            if (propertyID != null)
            {
                ViewData["PropertyID"] = propertyID.Value;
                viewModel.Offers = viewModel.Properties.Where(x => x.ID == propertyID).Single().Offers;
            }
            return View(viewModel);
        }

        // GET: Agents/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AgentName,Title")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var agent = await _context.Agent
            .Include(i => i.PublishedProperties).ThenInclude(i => i.Property)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (agent == null)
            {
                return NotFound();
            }
            PopulatePublishedPropertyData(agent);
            return View(agent);
        }
        private void PopulatePublishedPropertyData(Agent agent)
        {
            var allProperties = _context.Property;
            var publisherBooks = new HashSet<int>(agent.PublishedProperties.Select(c => c.PropertyID));
            var viewModel = new List<PublishedPropertyData>();
            foreach (var property in allProperties)
            {
                viewModel.Add(new PublishedPropertyData
                {
                    PropertyID = property.ID,
                    PropertyType = property.PropertyType,
                    IsAvailable = publisherBooks.Contains(property.ID)
                });
            }
            ViewData["Books"] = viewModel;
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedProperties)
        {
            if (id == null)
            {
                return NotFound();
            }
            var agentToUpdate = await _context.Agent
            .Include(i => i.PublishedProperties)
            .ThenInclude(i => i.Property)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Agent>(
            agentToUpdate,
            "",
            i => i.AgentName, i => i.Title))
            {
                UpdatePublishedPropertyData(selectedProperties, agentToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePublishedPropertyData(selectedProperties, agentToUpdate);
            PopulatePublishedPropertyData(agentToUpdate);
            return View(agentToUpdate);
        }
        private void UpdatePublishedPropertyData(string[] selectedProperties, Agent agentToUpdate)
        {
            if (selectedProperties == null)
            {
                agentToUpdate.PublishedProperties = new List<PublishedProperty>();
                return;
            }
            var selectedPropertiesHS = new HashSet<string>(selectedProperties);
            var publishedProperties = new HashSet<int>
            (agentToUpdate.PublishedProperties.Select(c => c.Property.ID));
            foreach (var property in _context.Property)
            {
                if (selectedPropertiesHS.Contains(property.ID.ToString()))
                {
                    if (!publishedProperties.Contains(property.ID))
                    {
                        agentToUpdate.PublishedProperties.Add(new PublishedProperty
                        {
                            AgentID = agentToUpdate.ID,
                            PropertyID = property.ID
                        });
                    }
                }
                else
                {
                    if (publishedProperties.Contains(property.ID))
                    {
                        PublishedProperty propertyToRemove = agentToUpdate.PublishedProperties.FirstOrDefault(i
                        => i.PropertyID == property.ID);
                        _context.Remove(propertyToRemove);
                    }
                }
            }
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            if (agent != null)
            {
                _context.Agent.Remove(agent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agent.Any(e => e.ID == id);
        }
    }
}
