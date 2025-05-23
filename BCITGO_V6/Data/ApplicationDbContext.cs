﻿using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BCITGO_V6.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Fix: Inherit from IdentityDbContext or DbContext  
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets  
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Ride> Ride { get; set; } = default!;
        public DbSet<Booking> Booking { get; set; } = default!;
        public DbSet<Review> Review { get; set; } = default!;
        public DbSet<SupportTicket> SupportTicket { get; set; } = default!;
        public DbSet<Donation> Donation { get; set; } = default!;
        public DbSet<RidePoints> RidePoints { get; set; } = default!;
        public DbSet<Invite> Invite { get; set; } = default!;
        public DbSet<Store> Store { get; set; } = default!;
        public DbSet<PointClaim> PointClaim { get; set; } = default!;
        public DbSet<RidePointsSummary> RidePointsSummary { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users"); //
            modelBuilder.Entity<Ride>().ToTable("Rides"); // 
            modelBuilder.Entity<Booking>().ToTable("Bookings"); // 
            modelBuilder.Entity<Review>().ToTable("Reviews"); // 
            modelBuilder.Entity<SupportTicket>().ToTable("SupportTickets"); // 
            modelBuilder.Entity<Donation>().ToTable("Donations");
            modelBuilder.Entity<RidePoints>().ToTable("RidePoints"); // 
            modelBuilder.Entity<Invite>().ToTable("Invites"); // 
            modelBuilder.Entity<Store>().ToTable("Stores"); // 
            modelBuilder.Entity<PointClaim>().ToTable("PointClaims"); // 
            modelBuilder.Entity<RidePointsSummary>().ToTable("RidePointsSummary"); // 

            // Relationships  

            modelBuilder.Entity<RidePointsSummary>()
                .HasOne(rps => rps.User)
                .WithMany(u => u.RidePointsSummary)
                .HasForeignKey(rps => rps.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Driver)
                .WithMany(u => u.Rides)
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RidePoints>()
                .HasOne(p => p.User)
                .WithMany(u => u.RidePoints)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RidePoints>()
                .HasOne(p => p.Ride)
                .WithMany(r => r.RidePoints)
                .HasForeignKey(p => p.RideId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<SupportTicket>()
                .HasOne(s => s.User)
                .WithMany(u => u.SupportTickets)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.User)
                .WithMany(u => u.Donations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invite>()
                .HasOne(i => i.InviterUser)
                .WithMany(u => u.InvitesSent)
                .HasForeignKey(i => i.InviterUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PointClaim>()
                .HasOne(pc => pc.User)
                .WithMany(u => u.PointClaims)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PointClaim>()
                .HasOne(pc => pc.Store)
                .WithMany(s => s.PointClaims)
                .HasForeignKey(pc => pc.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Fix for SQLite: ConcurrencyStamp nvarchar(max) not supported  
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>(entity =>
            {
                entity.Property(r => r.ConcurrencyStamp).HasMaxLength(256);
            });
        }
    }
}