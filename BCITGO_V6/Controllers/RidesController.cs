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
    public class RidesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RidesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rides
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ride.Include(r => r.Driver);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rides/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ride = await _context.Ride
                .Include(r => r.Driver)
                .FirstOrDefaultAsync(m => m.RideId == id);
            if (ride == null)
            {
                return NotFound();
            }

            return View(ride);
        }

        // GET: Rides/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Rides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RideId,DriverId,StartLocation,EndLocation,DepartureDate,DepartureTime,PricePerSeat,AvailableSeats,Notes,LuggageAllowed,PetsAllowed,Status,CreatedAt,UpdatedAt")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                ride.RideId = Guid.NewGuid();
                _context.Add(ride);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.User, "UserId", "UserId", ride.DriverId);
            return View(ride);
        }

        // GET: Rides/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ride = await _context.Ride.FindAsync(id);
            if (ride == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.User, "UserId", "UserId", ride.DriverId);
            return View(ride);
        }

        // POST: Rides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RideId,DriverId,StartLocation,EndLocation,DepartureDate,DepartureTime,PricePerSeat,AvailableSeats,Notes,LuggageAllowed,PetsAllowed,Status,CreatedAt,UpdatedAt")] Ride ride)
        {
            if (id != ride.RideId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ride);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RideExists(ride.RideId))
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
            ViewData["DriverId"] = new SelectList(_context.User, "UserId", "UserId", ride.DriverId);
            return View(ride);
        }

        // GET: Rides/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ride = await _context.Ride
                .Include(r => r.Driver)
                .FirstOrDefaultAsync(m => m.RideId == id);
            if (ride == null)
            {
                return NotFound();
            }

            return View(ride);
        }

        // POST: Rides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ride = await _context.Ride.FindAsync(id);
            if (ride != null)
            {
                _context.Ride.Remove(ride);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RideExists(Guid id)
        {
            return _context.Ride.Any(e => e.RideId == id);
        }
    }
}
