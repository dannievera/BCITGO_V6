using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;


namespace BCITGO_V6
{
    public class Program
    {
        //public static void Main(string[] args)
        public static async Task Main(string[] args)

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
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();




            // Add Razor Pages for Identity 
            builder.Services.AddRazorPages();

            // Add Session service
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });




            var app = builder.Build();

            // Apply migrations and ensure the database is created
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                // Apply pending migrations
                context.Database.Migrate();

                // Seed any default data
                DbInitializer.Initialize(context);

                // ✅ Add this block
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var adminEmail = "admin@bcitgo.com"; // or whatever your admin email is
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser != null && !(await userManager.IsInRoleAsync(adminUser, "Admin")))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
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

            app.UseAuthentication();
            app.UseAuthorization();

            // Add Session middleware
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();

        }
    }

}
