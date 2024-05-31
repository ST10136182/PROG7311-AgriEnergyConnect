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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employees>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<Farmers>()
        .HasOne(f => f.User)
        .WithMany()
        .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<Products>()
            .HasOne(p => p.Farmer)
            .WithMany()
            .HasForeignKey(p => p.FarmersId);

    }


}

