using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BCITGO_V6.Data
{
    public class ApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSets
        public DbSet<User> User { get; set; }
        public DbSet<Ride> Ride { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<SupportTicket> SupportTicket { get; set; }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<Invite> Invite { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<PointClaim> PointClaim { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            modelBuilder.Entity<User>().ToTable("User");

            // Rides
            modelBuilder.Entity<Ride>().ToTable("Ride");

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Driver)
                .WithMany(u => u.Rides)
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bookings
            modelBuilder.Entity<Booking>().ToTable("Bookings");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Ride)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RideId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Passenger)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reviews
            modelBuilder.Entity<Review>().ToTable("Reviews");

            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.Ride)
                .WithMany(r => r.Reviews)
                .HasForeignKey(rv => rv.RideId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.Reviewer)
                .WithMany(u => u.ReviewsWritten)
                .HasForeignKey(rv => rv.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.ReviewedUser)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(rv => rv.ReviewedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Support Tickets
            modelBuilder.Entity<SupportTicket>().ToTable("SupportTickets");

            modelBuilder.Entity<SupportTicket>()
                .HasOne(s => s.User)
                .WithMany(u => u.SupportTickets)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Donations
            modelBuilder.Entity<Donation>().ToTable("Donations");

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.User)
                .WithMany(u => u.Donations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            // Points
            modelBuilder.Entity<Point>().ToTable("Points");

            modelBuilder.Entity<Point>()
                .HasOne(d => d.User)
                .WithMany(u => u.Point)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Point>()
                .HasOne(d => d.Ride)
                .WithMany(u => u.Point)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Invites
            modelBuilder.Entity<Invite>().ToTable("Invites");

            // Stores
            modelBuilder.Entity<Store>().ToTable("Stores");

            // PointClaims
            modelBuilder.Entity<PointClaim>().ToTable("PointClaims");

            // Points








            // Fix for SQLite: ConcurrencyStamp nvarchar(max) not supported
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>(entity =>
            {
                entity.Property(r => r.ConcurrencyStamp).HasMaxLength(256);
            });
        }

    }
}