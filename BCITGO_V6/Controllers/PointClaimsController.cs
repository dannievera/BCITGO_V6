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
    public class PointClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PointClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PointClaims
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PointClaim.Include(p => p.Store).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PointClaims/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointClaim = await _context.PointClaim
                .Include(p => p.Store)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PointClaimId == id);
            if (pointClaim == null)
            {
                return NotFound();
            }

            return View(pointClaim);
        }

        // GET: PointClaims/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreId");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: PointClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PointClaimId,UserId,StoreId,AmountClaimed,Status,CreatedAt")] PointClaim pointClaim)
        {
            if (ModelState.IsValid)
            {
                pointClaim.PointClaimId = Guid.NewGuid();
                _context.Add(pointClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreId", pointClaim.StoreId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", pointClaim.UserId);
            return View(pointClaim);
        }

        // GET: PointClaims/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointClaim = await _context.PointClaim.FindAsync(id);
            if (pointClaim == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreId", pointClaim.StoreId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", pointClaim.UserId);
            return View(pointClaim);
        }

        // POST: PointClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PointClaimId,UserId,StoreId,AmountClaimed,Status,CreatedAt")] PointClaim pointClaim)
        {
            if (id != pointClaim.PointClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointClaimExists(pointClaim.PointClaimId))
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
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "StoreId", pointClaim.StoreId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", pointClaim.UserId);
            return View(pointClaim);
        }

        // GET: PointClaims/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointClaim = await _context.PointClaim
                .Include(p => p.Store)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PointClaimId == id);
            if (pointClaim == null)
            {
                return NotFound();
            }

            return View(pointClaim);
        }

        // POST: PointClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pointClaim = await _context.PointClaim.FindAsync(id);
            if (pointClaim != null)
            {
                _context.PointClaim.Remove(pointClaim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointClaimExists(Guid id)
        {
            return _context.PointClaim.Any(e => e.PointClaimId == id);
        }
    }
}
