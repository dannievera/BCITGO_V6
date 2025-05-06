using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;


namespace BCITGO_V6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Get the connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container. 
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity services -dvb 0505
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // Allow immediate login without email confirmation - dvb 0505 

                // DEVELOPMENT ONLY > make password easier  - dvb 0505
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();



            // Add Razor Pages for Identity 
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Apply migrations and ensure the database is created
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                // Apply migrations instead of EnsureCreated 
                context.Database.Migrate();  // Apply pending migrations to the database

                // Initialize database with seed data 
                DbInitializer.Initialize(context);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else //
            {
                app.UseDeveloperExceptionPage(); // Show detailed errors in development mode
                app.UseMigrationsEndPoint(); // Allow database migrations in development
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Required for Identity 
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages(); // Enable Razor Pages for Identity 

            app.Run();
        }
    }
}
