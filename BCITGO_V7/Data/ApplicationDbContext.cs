using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BCITGO_V6.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<PointRedemption> PointRedemption { get; set; } = default!;
        public DbSet<RidePoint> RidePoint { get; set; } = default!;
        public DbSet<Store> Store { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Invite> Invite { get; set; } = default!;
        public DbSet<Ride> Ride { get; set; } = default!;
        public DbSet<SupportTicket> SupportTicket { get; set; } = default!;
        public DbSet<Booking> Booking { get; set; } = default!;
        public DbSet<Review> Review { get; set; } = default!;
        public DbSet<SupportComment> SupportComment { get; set; } = default!;
        public DbSet<Donation> Donation { get; set; } = default!;
        public DbSet<Notification> Notification { get; set; } = default!;





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TABLE NAMES HERE

            modelBuilder.Entity<PointRedemption>().ToTable("POINT_REDEMPTIONS");
            modelBuilder.Entity<RidePoint>().ToTable("RIDEPOINTS");
            modelBuilder.Entity<Store>().ToTable("STORES");
            modelBuilder.Entity<User>().ToTable("USERS");
            modelBuilder.Entity<Invite>().ToTable("INVITES");
            modelBuilder.Entity<Ride>().ToTable("RIDES");
            modelBuilder.Entity<SupportTicket>().ToTable("SUPPORT_TICKETS");
            modelBuilder.Entity<Booking>().ToTable("BOOKINGS");
            modelBuilder.Entity<Review>().ToTable("REVIEWS");
            modelBuilder.Entity<SupportComment>().ToTable("SUPPORT_COMMENTS");
            modelBuilder.Entity<Donation>().ToTable("DONATION");


            // Relationships  
            modelBuilder.Entity<User>()
                .Property(u => u.IdentityUserId)
                .HasMaxLength(100);

            // USERS > RIDE POINTS (user can have multiple ride points)
            modelBuilder.Entity<RidePoint>()
                .HasOne(rp => rp.User)
                .WithMany()  // User can have multiple ride points
                .HasForeignKey(rp => rp.UserId)  // Foreign key on UserId
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of user if ride points exist

            // USERS > SUPPORT TICKETS (user can have multiple support tickets)
            modelBuilder.Entity<SupportTicket>()
                .HasOne(st => st.User)
                .WithMany()  // User can have multiple support tickets
                .HasForeignKey(st => st.UserId)  // Foreign key on UserId
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of user if support tickets exist



            // Fix for SQLite: ConcurrencyStamp nvarchar(max) not supported  
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>(entity =>
            {
                entity.Property(r => r.ConcurrencyStamp).HasMaxLength(256);
            });
        }
    }
}