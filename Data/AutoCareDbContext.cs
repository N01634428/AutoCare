using AutoCareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoCareAPI.Data
{
    public class AutoCareDbContext : DbContext
    {
        public AutoCareDbContext(DbContextOptions<AutoCareDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Appointments)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
