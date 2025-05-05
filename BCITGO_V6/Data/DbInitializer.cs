using BCITGO_V6.Data;
using BCITGO_V6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;

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


                // --- SEED ADMIN USER ---
                if (!context.User.Any(u => u.Role == "Admin"))
                {
                    var admin = new User
                    {
                        UserId = Guid.NewGuid(),
                        FullName = "Dannielyn Buenviaje",
                        Email = "dbuenviaje@my.bcit.ca",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("superadmin"), //Hashed password for security
                        Role = "Admin",
                        EmailVerified = true,
                        AccountStatus = "Active",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    context.User.Add(admin);
                    context.SaveChanges();
                }

                // --- SEED STORE USER ACCOUNT ---
                if (!context.User.Any(u => u.Role == "Store"))
                {
                    var storeUser = new User
                    {
                        UserId = Guid.NewGuid(),
                        FullName = "BCIT Bookstore",
                        Email = "bookstore@bcitgo.ca",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("store123"),
                        Role = "Store",
                        EmailVerified = true,
                        AccountStatus = "Active",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    context.User.Add(storeUser);
                    context.SaveChanges();

                    // --- SEED DEFAULT STORE ---
                    if (!context.Store.Any())
                    {
                        var store = new Store
                        {
                            StoreId = Guid.NewGuid(),
                            StoreName = "BCIT Bookstore",
                            CreatedAt = DateTime.UtcNow
                        };

                        context.Store.Add(store);
                        context.SaveChanges();
                    }
                }


                // --- OPTIONAL: SEED Ride Points placeholder ---
                if (!context.RidePoints.Any())
                {
                    // Optional: Add initial ride points if needed
                }

                // --- SEED DONE ---
            }
        }
    }
}
