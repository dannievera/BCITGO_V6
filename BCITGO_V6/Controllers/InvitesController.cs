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
    public class InvitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invite.Include(i => i.InviterUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invites/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invite = await _context.Invite
                .Include(i => i.InviterUser)
                .FirstOrDefaultAsync(m => m.InviteId == id);
            if (invite == null)
            {
                return NotFound();
            }

            return View(invite);
        }

        // GET: Invites/Create
        public IActionResult Create()
        {
            ViewData["InviterUserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Invites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InviteId,InviterUserId,InviteEmail,Status,CreatedAt")] Invite invite)
        {
            if (ModelState.IsValid)
            {
                invite.InviteId = Guid.NewGuid();
                _context.Add(invite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InviterUserId"] = new SelectList(_context.User, "UserId", "UserId", invite.InviterUserId);
            return View(invite);
        }

        // GET: Invites/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invite = await _context.Invite.FindAsync(id);
            if (invite == null)
            {
                return NotFound();
            }
            ViewData["InviterUserId"] = new SelectList(_context.User, "UserId", "UserId", invite.InviterUserId);
            return View(invite);
        }

        // POST: Invites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InviteId,InviterUserId,InviteEmail,Status,CreatedAt")] Invite invite)
        {
            if (id != invite.InviteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InviteExists(invite.InviteId))
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
            ViewData["InviterUserId"] = new SelectList(_context.User, "UserId", "UserId", invite.InviterUserId);
            return View(invite);
        }

        // GET: Invites/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invite = await _context.Invite
                .Include(i => i.InviterUser)
                .FirstOrDefaultAsync(m => m.InviteId == id);
            if (invite == null)
            {
                return NotFound();
            }

            return View(invite);
        }

        // POST: Invites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invite = await _context.Invite.FindAsync(id);
            if (invite != null)
            {
                _context.Invite.Remove(invite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InviteExists(Guid id)
        {
            return _context.Invite.Any(e => e.InviteId == id);
        }
    }
}
