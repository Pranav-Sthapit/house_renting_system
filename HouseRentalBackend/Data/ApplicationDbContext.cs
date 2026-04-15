using HouseRentalBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentalBackend.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

        public DbSet<Renter> Renters { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public DbSet<RenterInfo> RenterInfos{ get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Person>().HasIndex(p => p.Username).IsUnique();
            modelBuilder.Entity<Person>().HasIndex(p => p.Contact).IsUnique();
            modelBuilder.Entity<Rental>().HasKey(r => new { r.RenterId, r.PropertyId });

            modelBuilder.Entity<Renter>().HasOne(r => r.RenterInfo).WithOne(ri => ri.Renter).HasForeignKey<RenterInfo>(ri => ri.RenterId);
            modelBuilder.Entity<Owner>().HasMany(o => o.Properties).WithOne(p => p.Owner).HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Rental>().HasOne(rl => rl.Renter).WithMany(r => r.Rentals).HasForeignKey(rl=>rl.RenterId);
            modelBuilder.Entity<Rental>().HasOne(rl => rl.Property).WithMany(p => p.Rentals).HasForeignKey(rl => rl.PropertyId);
        }
    }
}
