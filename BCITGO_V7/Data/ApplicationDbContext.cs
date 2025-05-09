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


            //// STORES > POINT REDEMPTIONS
            //modelBuilder.Entity<PointRedemption>()
            //    .HasOne(pr => pr.Store)
            //    .WithMany()
            //    .HasForeignKey(pr => pr.StoreId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > POINT REDEMPTIONS
            //modelBuilder.Entity<PointRedemption>()
            //    .HasOne(pr => pr.User)
            //    .WithMany()
            //    .HasForeignKey(pr => pr.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > RIDEPOINTS
            //modelBuilder.Entity<RidePoint>()
            //    .HasOne(rp => rp.User)
            //    .WithMany()
            //    .HasForeignKey(rp => rp.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > DONATIONS
            //modelBuilder.Entity<Donation>()
            //    .HasOne(d => d.User)
            //    .WithMany()
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > RIDES
            //modelBuilder.Entity<Ride>()
            //    .HasOne(r => r.User)
            //    .WithMany()
            //    .HasForeignKey(r => r.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > SUPPORT TICKETS
            //modelBuilder.Entity<SupportTicket>()
            //    .HasOne(st => st.User)
            //    .WithMany()
            //    .HasForeignKey(st => st.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// SUPPORT TICKETS > SUPPORT COMMENTS
            //modelBuilder.Entity<SupportComment>()
            //    .HasOne(sc => sc.SupportTicket)
            //    .WithMany()
            //    .HasForeignKey(sc => sc.TicketId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// USERS > SUPPORT COMMENTS
            //modelBuilder.Entity<SupportComment>()
            //    .HasOne(sc => sc.User)
            //    .WithMany()
            //    .HasForeignKey(sc => sc.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > INVITES (Inviter)
            //modelBuilder.Entity<Invite>()
            //    .HasOne(i => i.User)
            //    .WithMany()
            //    .HasForeignKey(i => i.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > INVITES (Invitee)
            //modelBuilder.Entity<Invite>()
            //    .HasOne(i => i.Invitee)
            //    .WithMany()
            //    .HasForeignKey(i => i.InviteeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// RIDES > BOOKINGS
            //modelBuilder.Entity<Booking>()
            //    .HasOne(b => b.Ride)
            //    .WithMany()
            //    .HasForeignKey(b => b.RideId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > BOOKINGS
            //modelBuilder.Entity<Booking>()
            //    .HasOne(b => b.User)
            //    .WithMany()
            //    .HasForeignKey(b => b.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// RIDES > REVIEWS
            //modelBuilder.Entity<Review>()
            //    .HasOne(rv => rv.Ride)
            //    .WithMany()
            //    .HasForeignKey(rv => rv.RideId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > REVIEWS (Reviewer)
            //modelBuilder.Entity<Review>()
            //    .HasOne(rv => rv.Reviewer)
            //    .WithMany()
            //    .HasForeignKey(rv => rv.ReviewerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// USERS > REVIEWS (Reviewee)
            //modelBuilder.Entity<Review>()
            //    .HasOne(rv => rv.Reviewee)
            //    .WithMany()
            //    .HasForeignKey(rv => rv.RevieweeId)
            //    .OnDelete(DeleteBehavior.Restrict);


            // Fix for SQLite: ConcurrencyStamp nvarchar(max) not supported  
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>(entity =>
            {
                entity.Property(r => r.ConcurrencyStamp).HasMaxLength(256);
            });
        }
    }
}