﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using BCITGO_V6.Data;
using BCITGO_V6.Models;


namespace BCITGO_V6.Pages.Rides
{
    public class PostRideModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public PostRideModel(ApplicationDbContext context)
            : base(context) // Call the base constructor
        {
            _context = context;
        }

        [BindProperty, Required]
        public string StartLocation { get; set; }

        [BindProperty, Required]
        public string EndLocation { get; set; }

        [BindProperty, Required]
        public DateTime DepartureDate { get; set; }

        [BindProperty, Required]
        public TimeSpan DepartureTime { get; set; }

        [BindProperty, Required, Range(1, 1000)]
        public decimal PricePerSeat { get; set; }

        [BindProperty, Required, Range(1, 8)]
        public int TotalSeats { get; set; }

        [BindProperty]
        public string? Notes { get; set; }

        [BindProperty]
        public bool LuggageAllowed { get; set; }

        [BindProperty]
        public bool PetsAllowed { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Set default DepartureDate to today only if not already set
            if (DepartureDate == default(DateTime))
            {
                DepartureDate = DateTime.Today;
            }

            var identityId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var appUser = _context.User.FirstOrDefault(u => u.IdentityUserId == identityId);

            if (appUser != null)
            {
                var unreadCount = _context.Notification
                    .Where(n => n.UserId == appUser.UserId && !n.IsRead)
                    .Count();

                ViewData["UnreadCount"] = unreadCount;
            }

            LoadUnreadCount(); // under onget added

        }


        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    ErrorMessage = "Please complete all required fields.";
            //    return Page();
            //}

            //var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            //var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);

            //if (user == null)
            //{
            //    ErrorMessage = "User not found.";
            //    return Page();
            //}

            // Validate
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please complete all required fields properly.";
                return Page();
            }

            // Validate Departure Date and Time
            var rideDateTime = DepartureDate.Date + DepartureTime;

            if (rideDateTime <= DateTime.Now)
            {
                ModelState.AddModelError("DepartureDate", "Please select a departure date and time that is later than now.");
                return Page();
            }

            // Extra validation (for empty input or only whitespace)
            if (string.IsNullOrWhiteSpace(StartLocation) || string.IsNullOrWhiteSpace(EndLocation))
            {
                ErrorMessage = "Start Location and End Location cannot be empty.";
                return Page();
            }


            // Clean up (remove trailing/leading spaces, trim)
            StartLocation = StartLocation.Trim();
            EndLocation = EndLocation.Trim();

            // Check user
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);

            if (user == null)
            {
                ErrorMessage = "User not found.";
                return Page();
            }

            //Create Ride

            var ride = new Ride
            {
                StartLocation = StartLocation,
                EndLocation = EndLocation,
                DepartureDate = DepartureDate,
                DepartureTime = DepartureTime,
                PricePerSeat = PricePerSeat,
                TotalSeats = TotalSeats, //
                Notes = Notes,
                LuggageAllowed = LuggageAllowed,
                PetsAllowed = PetsAllowed,
                CreatedAt = DateTime.Now,
                UserId = user.UserId,
                Status = "Active"
            };


            _context.Ride.Add(ride);
            await _context.SaveChangesAsync();

            SuccessMessage = "Ride posted successfully! You can now manage your ride in 'My Rides.'";
            return Page();
        }
    }
}
