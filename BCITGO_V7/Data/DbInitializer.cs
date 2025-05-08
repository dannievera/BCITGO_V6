using BCITGO_V6.Data;
using BCITGO_V6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;

namespace BCITGO_V6.Data
{
    public class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context)
        {
            {
                // Ensure the database is created and migrated
                var databaseFacade = context.Database as DatabaseFacade;
                databaseFacade?.Migrate();

                // Check if any data exists to avoid duplication
                if (context.PointRedemption.Any() || context.RidePoint.Any() || context.Store.Any() ||
                    context.User.Any() || context.Invite.Any() || context.Ride.Any() ||
                    context.SupportTicket.Any() || context.Booking.Any() || context.Review.Any() ||
                    context.SupportComment.Any())
                {
                    return; // DB has been seeded
                }

                //---SEEDING STARTS HERE---//

                // USERS
                var user1 = new User
                {
                    FullName = "User Seed 1",
                    Email = "userseed1@my.bcit.ca",
                    PasswordHash = "hash1",
                    PhoneNumber = "123-456-7890",
                    Bio = "Enjoys sharing rides.",
                    ProfilePicture = "default-profile1.png",
                    Role = "User",
                    Status = "Active",
                    CreatedAt = DateTime.Now,
                    LastActiveAt = DateTime.Now
                };

                var user2 = new User
                {
                    FullName = "Admin Seed 1",
                    Email = "adminseed1@my.bcit.ca",
                    PasswordHash = "hash2",
                    PhoneNumber = "321-654-0987",
                    Bio = "Loves meeting new people.",
                    ProfilePicture = "default-profile2.png",
                    Role = "Admin",
                    Status = "Active",
                    CreatedAt = DateTime.Now,
                    LastActiveAt = DateTime.Now
                };

                context.User.AddRange(user1, user2);
                context.SaveChanges();

                // STORES
                var store1 = new Store
                {
                    StoreName = "Official BCIT Store",
                    Email = "store@bcit.ca",
                    PasswordHash = "storehash",
                    CreatedAt = DateTime.Now
                };

                context.Store.Add(store1);
                context.SaveChanges();


                // INVITES
                var invite1 = new Invite
                {
                    UserId = user1.UserId,
                    InviteeId = user2.UserId,
                    InviteeEmail = user2.Email,
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                context.Invite.Add(invite1);
                context.SaveChanges();

                // RIDES
                var ride1 = new Ride
                {
                    UserId = user1.UserId,
                    StartLocation = "Metrotown",
                    EndLocation = "Richmond",
                    DepartureDate = DateTime.Today.AddDays(1),
                    DepartureTime = new TimeSpan(8, 30, 0),
                    PricePerSeat = 15,
                    //AvailableSeats = 3,
                    Notes = "No heavy luggage please.",
                    LuggageAllowed = true,
                    PetsAllowed = false,
                    Status = "Scheduled",
                    CreatedAt = DateTime.Now
                };

                context.Ride.Add(ride1);
                context.SaveChanges();

                // BOOKINGS
                var booking1 = new Booking
                {
                    RideId = ride1.RideId,
                    UserId = user2.UserId,
                    SeatsBooked = 2,
                    BookingMessage = "Looking forward to the ride!",
                    Status = "Confirmed",
                    CreatedAt = DateTime.Now
                };

                context.Booking.Add(booking1);
                context.SaveChanges();

                // REVIEWS
                var review1 = new Review
                {
                    RideId = ride1.RideId,
                    ReviewerId = user2.UserId,
                    RevieweeId = user1.UserId,
                    Rating = 5,
                    ReviewText = "Excellent ride and friendly driver!",
                    CreatedAt = DateTime.Now
                };

                context.Review.Add(review1);
                context.SaveChanges();

                // POINT REDEMPTIONS
                var redemption1 = new PointRedemption
                {
                    StoreId = store1.StoreId,
                    UserId = user1.UserId,
                    PointsRedeemed = 10,
                    Status = "Accept",
                    CreatedAt = DateTime.Now
                };

                var redemption2 = new PointRedemption
                {
                    StoreId = store1.StoreId,
                    UserId = user1.UserId,
                    PointsRedeemed = 10,
                    Status = "Reject",
                    CreatedAt = DateTime.Now
                };


                context.PointRedemption.AddRange(redemption1, redemption2);
                context.SaveChanges();


                // RIDEPOINTS
                var ridePoint1 = new RidePoint
                {
                    UserId = user2.UserId,
                    Points = 1,
                    Reason = "Completed a ride",
                    CreatedAt = DateTime.Now
                };

                context.RidePoint.Add(ridePoint1);
                context.SaveChanges();

                // SUPPORT TICKETS
                var ticket1 = new SupportTicket
                {
                    UserId = user2.UserId,
                    Details = "Issue with booking confirmation",
                    Status = "Open",
                    CreatedAt = DateTime.Now
                };

                context.SupportTicket.Add(ticket1);
                context.SaveChanges();

                // SUPPORT COMMENTS
                var comment1 = new SupportComment
                {
                    TicketId = ticket1.TicketId,
                    UserId = user1.UserId,
                    CommentText = "We are investigating the issue, please wait.",
                    CreatedAt = DateTime.Now
                };

                context.SupportComment.Add(comment1);
                context.SaveChanges();

                // DONATION
                var donation1 = new Donation
                {
                    UserId = user1.UserId,
                    Amount = 10,
                    Message = "Thank you BCITGO!",
                    CreatedAt = DateTime.Now
                };

                context.Donation.Add(donation1);
                context.SaveChanges();


                // --- SEEDING ENDS HERE ---//
            }
        }
    }
}