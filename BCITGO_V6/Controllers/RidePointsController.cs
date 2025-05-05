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
    public class RidePointsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RidePointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RidePoints
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RidePoints.Include(r => r.Ride).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RidePoints/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ridePoints = await _context.RidePoints
                .Include(r => r.Ride)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RidePointsId == id);
            if (ridePoints == null)
            {
                return NotFound();
            }

            return View(ridePoints);
        }

        // GET: RidePoints/Create
        public IActionResult Create()
        {
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: RidePoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RidePointsId,UserId,RideId,Source,Amount,CreatedAt")] RidePoints ridePoints)
        {
            if (ModelState.IsValid)
            {
                ridePoints.RidePointsId = Guid.NewGuid();
                _context.Add(ridePoints);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId", ridePoints.RideId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", ridePoints.UserId);
            return View(ridePoints);
        }

        // GET: RidePoints/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ridePoints = await _context.RidePoints.FindAsync(id);
            if (ridePoints == null)
            {
                return NotFound();
            }
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId", ridePoints.RideId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", ridePoints.UserId);
            return View(ridePoints);
        }

        // POST: RidePoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RidePointsId,UserId,RideId,Source,Amount,CreatedAt")] RidePoints ridePoints)
        {
            if (id != ridePoints.RidePointsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ridePoints);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RidePointsExists(ridePoints.RidePointsId))
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
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId", ridePoints.RideId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", ridePoints.UserId);
            return View(ridePoints);
        }

        // GET: RidePoints/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ridePoints = await _context.RidePoints
                .Include(r => r.Ride)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RidePointsId == id);
            if (ridePoints == null)
            {
                return NotFound();
            }

            return View(ridePoints);
        }

        // POST: RidePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ridePoints = await _context.RidePoints.FindAsync(id);
            if (ridePoints != null)
            {
                _context.RidePoints.Remove(ridePoints);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RidePointsExists(Guid id)
        {
            return _context.RidePoints.Any(e => e.RidePointsId == id);
        }
    }
}
