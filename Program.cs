using Agri_Energy_Connect_Platform.Data;
using Agri_Energy_Connect_Platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Bypass SSL certificate validation
System.Net.ServicePointManager.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Create roles and seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
    await SeedDatabase(services);
}

app.Run();

// Method to create roles
async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Farmer", "Employee" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

 static async Task SeedDatabase(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

    // Add employee user
    var employeeUser = new IdentityUser { UserName = "employee@demo.com", Email = "employee@demo.com", EmailConfirmed = true };
    var employeeResult = await userManager.CreateAsync(employeeUser, "Employee123!");
    if (employeeResult.Succeeded)
    {
        await userManager.AddToRoleAsync(employeeUser, "Employee");
    }

    // Add farmer user
    var farmerUser = new IdentityUser { UserName = "farmer@demo.com", Email = "farmer@demo.com", EmailConfirmed = true };
    var farmerResult = await userManager.CreateAsync(farmerUser, "Farmer123!");
    if (farmerResult.Succeeded)
    {
        await userManager.AddToRoleAsync(farmerUser, "Farmer");

        // Add farmer profile
        var farmer = new Farmers
        {
            FullName = "Demo Farmer",
            ContactNumber = "1234567890",
            Address = "Demo Address",
            UserId = farmerUser.Id
        };
        context.Farmers.Add(farmer);
        await context.SaveChangesAsync();

        // Add products for the farmer
        var products = new List<Products>
            {
                new Products { ProductName = "Apples", Category = "Fruit", ProductionDate = DateTime.Now.AddDays(-10), FarmersId = farmer.FarmersId },
                new Products { ProductName = "Carrots", Category = "Vegetable", ProductionDate = DateTime.Now.AddDays(-20), FarmersId = farmer.FarmersId }
            };
        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }

    // Add employee profile
    var employee = new Employees
    {
        FullName = "Demo Employee",
        ContactNumber = "0987654321",
        Position = "Employee Position",
        UserId = employeeUser.Id
    };
    context.Employees.Add(employee);
    await context.SaveChangesAsync();
}


