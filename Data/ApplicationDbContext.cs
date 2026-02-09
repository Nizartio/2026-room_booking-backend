using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomBooking> RoomBookings => Set<RoomBooking>();
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filter
            modelBuilder.Entity<Customer>()
                .HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Room>() 
                .HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<RoomBooking>()
                .HasQueryFilter(rb => !rb.IsDeleted);

            // Unique email for Customer
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}
