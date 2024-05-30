using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Agri_Energy_Connect_Platform.Models;
using Microsoft.AspNetCore.Identity;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Farmers> Farmers { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Employees> Employees { get; set; }

    
}

