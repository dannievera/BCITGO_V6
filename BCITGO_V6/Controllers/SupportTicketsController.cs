using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;

namespace BCITGO_V6.Controllers
{
    public class SupportTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupportTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SupportTickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SupportTicket.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SupportTickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportTicket = await _context.SupportTicket
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (supportTicket == null)
            {
                return NotFound();
            }

            return View(supportTicket);
        }

        // GET: SupportTickets/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: SupportTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,UserId,Details,Status,AdminNotes,CreatedAt")] SupportTicket supportTicket)
        {
            if (ModelState.IsValid)
            {
                supportTicket.TicketId = Guid.NewGuid();
                _context.Add(supportTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", supportTicket.UserId);
            return View(supportTicket);
        }

        // GET: SupportTickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportTicket = await _context.SupportTicket.FindAsync(id);
            if (supportTicket == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", supportTicket.UserId);
            return View(supportTicket);
        }

        // POST: SupportTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TicketId,UserId,Details,Status,AdminNotes,CreatedAt")] SupportTicket supportTicket)
        {
            if (id != supportTicket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supportTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportTicketExists(supportTicket.TicketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", supportTicket.UserId);
            return View(supportTicket);
        }

        // GET: SupportTickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supportTicket = await _context.SupportTicket
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (supportTicket == null)
            {
                return NotFound();
            }

            return View(supportTicket);
        }

        // POST: SupportTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supportTicket = await _context.SupportTicket.FindAsync(id);
            if (supportTicket != null)
            {
                _context.SupportTicket.Remove(supportTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupportTicketExists(Guid id)
        {
            return _context.SupportTicket.Any(e => e.TicketId == id);
        }
    }
}
