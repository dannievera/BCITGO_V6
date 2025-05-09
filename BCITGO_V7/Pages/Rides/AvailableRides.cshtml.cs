using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCITGO_V6.Pages.Rides
{
    public class AvailableRidesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AvailableRidesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ride> Rides { get; set; } = new List<Ride>();

        [BindProperty(SupportsGet = true)]
        public RideSearchFilter Filter { get; set; } = new RideSearchFilter();

        public void OnGet()
        {
            // Step 1 >> Load only active rides that are future

            //-----ORIGINAL CODE BELOW
            //var query = _context.Ride
            //    .Include(r => r.User)
            //    .Include(r => r.Bookings)
            //    .Where(r => r.Status == "Active" && r.DepartureTime != null)
            //    .ToList()
            //    //.Where(r => r.DepartureTime != default && (r.DepartureDate + r.DepartureTime) > DateTime.Now)
            //    .Where(r => (r.DepartureDate + r.DepartureTime) > DateTime.Now)
            //    .ToList();

            // ----UPDATED CODE BELOW
            var query = _context.Ride
                .Include(r => r.User)
                .Include(r => r.Bookings)
                .Where(r => r.Status == "Active")
                .ToList() // Pull to memory FIRST (added this so we can use TimeSpan safely)
                .Where(r =>
                    r.DepartureDate != default &&
                    r.DepartureTime != default &&
                    r.DepartureDate.Add(r.DepartureTime) > DateTime.Now)
                .ToList(); // Final in-memory list



            // Step 2 > Calculate Confirmed and Pending for ALL rides now
            foreach (var ride in query)
            {
                ride.BookedSeats = ride.Bookings.Where(b => b.Status == "Confirmed").Sum(b => b.SeatsBooked);
                ride.PendingRequests = ride.Bookings.Where(b => b.Status == "Pending").Sum(b => b.SeatsBooked);
            }

            // Step 3 >> Apply filters
            var filtered = query.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Filter.StartLocation))
            {
                var words = Filter.StartLocation.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    filtered = filtered.Where(r => r.StartLocation.Contains(word, StringComparison.OrdinalIgnoreCase));
                }
            }

            if (!string.IsNullOrWhiteSpace(Filter.EndLocation))
            {
                var words = Filter.EndLocation.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    filtered = filtered.Where(r => r.EndLocation.Contains(word, StringComparison.OrdinalIgnoreCase));
                }
            }

            if (Filter.DepartureDate != null)
                filtered = filtered.Where(r => r.DepartureDate == Filter.DepartureDate);

            if (Filter.MinPrice != null)
                filtered = filtered.Where(r => r.PricePerSeat >= Filter.MinPrice);

            if (Filter.MaxPrice != null)
                filtered = filtered.Where(r => r.PricePerSeat <= Filter.MaxPrice);

            if (Filter.SeatsNeeded != null)
            {
                filtered = filtered.Where(r => (r.TotalSeats - r.BookedSeats - r.PendingRequests) >= Filter.SeatsNeeded);
            }

            // Step 4 → Sorting
            if (Filter.SortBy == "price")
                filtered = filtered.OrderBy(r => r.PricePerSeat);
            else if (Filter.SortBy == "earliest")
                filtered = filtered.OrderBy(r => r.DepartureDate).ThenBy(r => r.DepartureTime);
            else
                filtered = filtered.OrderBy(r => r.CreatedAt);

            // Final Result
            Rides = filtered.ToList();
        }
    }

    public class RideSearchFilter
    {
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime? DepartureDate { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? SeatsNeeded { get; set; }
        public string SortBy { get; set; }
    }
}
