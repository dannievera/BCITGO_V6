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
    public class AvailableRidesModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public AvailableRidesModel(ApplicationDbContext context)
            : base(context) // Call the base constructor
        {
            _context = context;
        }

        public List<Ride> Rides { get; set; } = new List<Ride>();

        [BindProperty(SupportsGet = true)]
        public RideSearchFilter Filter { get; set; } = new RideSearchFilter();

        public void OnGet()
        {
            var now = DateTime.Now;

            // STEP 1: Load all ACTIVE rides with data into memory
            var query = _context.Ride
                .Include(r => r.User)
                .Include(r => r.Bookings)
                .Where(r => r.Status == "Active")
                .ToList(); // Pull to memory so we can safely use .Add()

            // STEP 2: Remove expired rides using DateTime logic safely

            //query = query
            //    .Where(r =>
            //        r.DepartureDate != default &&
            //        r.DepartureTime != default &&
            //        r.DepartureDate.Add(r.DepartureTime) > now
            //    ).ToList();

            query = query
            .Where(r =>
                r.DepartureDate != default &&
                r.DepartureDate.Add(r.DepartureTime) > now
            ).ToList();

            // STEP 3: Compute booked + pending
            foreach (var ride in query)
            {
                //ride.ConfirmedSeats = ride.Bookings?.Where(b => b.Status == "Confirmed").Sum(b => b.SeatsBooked) ?? 0;
                //ride.PendingSeats = ride.Bookings?.Where(b => b.Status == "Pending").Sum(b => b.SeatsBooked) ?? 0;
                ride.BookedSeats = ride.Bookings?.Where(b => b.Status == "Confirmed").Sum(b => b.SeatsBooked) ?? 0;
                ride.PendingRequests = ride.Bookings?.Where(b => b.Status == "Pending").Count() ?? 0;

            }

            // STEP 3.1: Remove rides with 0 or negative available seats
            query = query
                .Where(r => (r.TotalSeats - r.BookedSeats - r.PendingRequests) > 0)
                .ToList();

            // STEP 4: Apply Filters (same as before)
            var filtered = query.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Filter.StartLocation))
            {
                var words = Filter.StartLocation.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    filtered = filtered.Where(r =>
                        r.StartLocation.Contains(word, StringComparison.OrdinalIgnoreCase));
                }
            }

            if (!string.IsNullOrWhiteSpace(Filter.EndLocation))
            {
                var words = Filter.EndLocation.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    filtered = filtered.Where(r =>
                        r.EndLocation.Contains(word, StringComparison.OrdinalIgnoreCase));
                }
            }

            if (Filter.DepartureDate != null)
                filtered = filtered.Where(r => r.DepartureDate.Date == Filter.DepartureDate.Value.Date);

            if (Filter.MinPrice != null)
                filtered = filtered.Where(r => r.PricePerSeat >= Filter.MinPrice);

            if (Filter.MaxPrice != null)
                filtered = filtered.Where(r => r.PricePerSeat <= Filter.MaxPrice);

            if (Filter.SeatsNeeded != null)
                //filtered = filtered.Where(r => (r.TotalSeats - r.BookedSeats - r.PendingRequests) >= Filter.SeatsNeeded);
                filtered = filtered.Where(r => r.AvailableSeats >= Filter.SeatsNeeded);



            // STEP 5: Sorting
            if (Filter.SortBy == "price")
                filtered = filtered.OrderBy(r => r.PricePerSeat);
            else if (Filter.SortBy == "earliest")
                filtered = filtered.OrderBy(r => r.DepartureDate).ThenBy(r => r.DepartureTime);
            else
                filtered = filtered.OrderBy(r => r.CreatedAt);

            // Final output
            Rides = filtered.ToList();

            LoadUnreadCount(); // under onget added

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
