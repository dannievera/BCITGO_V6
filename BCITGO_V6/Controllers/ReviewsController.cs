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
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Review.Include(r => r.ReviewedUser).Include(r => r.Reviewer).Include(r => r.Ride);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.ReviewedUser)
                .Include(r => r.Reviewer)
                .Include(r => r.Ride)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["ReviewedUserId"] = new SelectList(_context.User, "UserId", "UserId");
            ViewData["ReviewerId"] = new SelectList(_context.User, "UserId", "UserId");
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,RideId,ReviewerId,ReviewedUserId,Rating,ReviewText,Status,CreatedAt")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.ReviewId = Guid.NewGuid();
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReviewedUserId"] = new SelectList(_context.User, "UserId", "UserId", review.ReviewedUserId);
            ViewData["ReviewerId"] = new SelectList(_context.User, "UserId", "UserId", review.ReviewerId);
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId", review.RideId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["ReviewedUserId"] = new SelectList(_context.User, "UserId", "UserId", review.ReviewedUserId);
            ViewData["ReviewerId"] = new SelectList(_context.User, "UserId", "UserId", review.ReviewerId);
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId", review.RideId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReviewId,RideId,ReviewerId,ReviewedUserId,Rating,ReviewText,Status,CreatedAt")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            ViewData["ReviewedUserId"] = new SelectList(_context.User, "UserId", "UserId", review.ReviewedUserId);
            ViewData["ReviewerId"] = new SelectList(_context.User, "UserId", "UserId", review.ReviewerId);
            ViewData["RideId"] = new SelectList(_context.Ride, "RideId", "RideId", review.RideId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.ReviewedUser)
                .Include(r => r.Reviewer)
                .Include(r => r.Ride)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review != null)
            {
                _context.Review.Remove(review);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(Guid id)
        {
            return _context.Review.Any(e => e.ReviewId == id);
        }
    }
}
