namespace BCITGO_V6.Data
{
    public class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context)

        // Check if any data exists to avoid duplication
        if (context.User.Any() || context.Ride.Any() || context.Booking.Any() ||
            context.Review.Any() || context.SupportTicker.Any() || context.Donation.Any() ||
            context.Point.Any() || context.Invite.Any() || context.Store.Any() ||
            context.PointClaim.Any() || 
        {
            return; // Database has been seeded
        }

        // Seed User
        // Seed Ride
        // Seed User
        // Seed User
        // Seed User
        // Seed User
        // Seed User
        // Seed User
        // Seed User
        // Seed User



    }
}
